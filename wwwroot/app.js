let state = null;

const money = new Intl.NumberFormat("vi-VN", {
  style: "currency",
  currency: "VND",
  maximumFractionDigits: 0
});

const statusText = {
  Draft: "Dang goi mon",
  SentToKitchen: "Da gui bep",
  Preparing: "Bep dang lam",
  Served: "Da phuc vu",
  Paid: "Da thanh toan",
  Closed: "Da dong"
};

document.getElementById("refreshButton").addEventListener("click", load);
document.getElementById("createOrderForm").addEventListener("submit", async event => {
  event.preventDefault();
  await postJson("/api/orders", {
    tableId: document.getElementById("tableSelect").value,
    guests: Number(document.getElementById("guestInput").value)
  });
  await load();
});

async function load() {
  const response = await fetch("/api/operations");
  state = await response.json();
  render();
}

function render() {
  document.getElementById("seatRange").textContent =
    `${state.profile.name}: ${state.summary.configuredSeats} ghe cau hinh, yeu cau ${state.profile.minSeats}-${state.profile.maxSeats}`;

  renderTables();
  renderMenu();
  renderInventory();
  renderOrders();
  renderSummary();
}

function renderTables() {
  const select = document.getElementById("tableSelect");
  select.innerHTML = state.tables
    .filter(table => !table.activeOrderId)
    .map(table => `<option value="${table.id}">${table.id} - ${table.seats} ghe</option>`)
    .join("");

  document.getElementById("tableGrid").innerHTML = state.tables.map(table => `
    <article class="tableTile ${table.activeOrderId ? "busy" : ""}">
      <strong>${table.id}</strong>
      <span class="muted">${table.seats} ghe</span>
      <span>${table.activeOrderId ? statusText[table.status] : "Trong"}</span>
    </article>
  `).join("");
}

function renderMenu() {
  document.getElementById("menuList").innerHTML = state.menu.map(item => `
    <div class="listItem">
      <div>
        <strong>${item.name}</strong>
        <div class="muted">${item.code}</div>
      </div>
      <span>${money.format(item.price)}</span>
    </div>
  `).join("");
}

function renderInventory() {
  document.getElementById("inventoryList").innerHTML = state.inventory.map(item => `
    <div class="listItem">
      <div>
        <strong>${item.name}</strong>
        <div class="muted">${item.sku}</div>
      </div>
      <span>${item.quantityOnHand - item.quantityReserved}/${item.quantityOnHand}</span>
    </div>
  `).join("");
}

function renderOrders() {
  const orders = document.getElementById("orders");
  if (state.openOrders.length === 0) {
    orders.innerHTML = `<p class="muted">Chua co order dang phuc vu.</p>`;
    return;
  }

  orders.innerHTML = state.openOrders.map(order => `
    <article class="orderCard">
      <div class="orderTitle">
        <div>
          <h2>${order.tableId} - ${order.guests} khach</h2>
          <div class="muted">${shortId(order.id)}</div>
        </div>
        <span class="badge">${statusText[order.status]}</span>
      </div>
      ${order.lines.map(line => `
        <div class="line">
          <span>${line.name} x ${line.quantity}</span>
          <strong>${money.format(line.lineTotal)}</strong>
        </div>
      `).join("")}
      <div class="line">
        <strong>Tong</strong>
        <strong>${money.format(order.total)}</strong>
      </div>
      ${order.status === "Draft" ? itemControls(order.id) : ""}
      <div class="actions">
        ${actionButton(order, "Draft", "Gui bep", "send")}
        ${actionButton(order, "SentToKitchen", "Bep nhan", "prepare")}
        ${actionButton(order, "Preparing", "Da phuc vu", "serve")}
        ${actionButton(order, "Served", "Thanh toan", "checkout")}
      </div>
    </article>
  `).join("");

  document.querySelectorAll("[data-action]").forEach(button => {
    button.addEventListener("click", () => runAction(button.dataset.orderId, button.dataset.action));
  });
  document.querySelectorAll("[data-add-item]").forEach(form => {
    form.addEventListener("submit", event => changeItem(event, "items"));
  });
  document.querySelectorAll("[data-remove-item]").forEach(form => {
    form.addEventListener("submit", event => changeItem(event, "items/remove"));
  });
}

function itemControls(orderId) {
  const options = state.menu.map(item => `<option value="${item.code}">${item.name}</option>`).join("");
  return `
    <form class="lineActions" data-add-item data-order-id="${orderId}">
      <select name="menuCode">${options}</select>
      <input name="quantity" type="number" min="1" value="1" aria-label="So luong them">
      <button type="submit">Them</button>
    </form>
    <form class="lineActions" data-remove-item data-order-id="${orderId}">
      <select name="menuCode">${options}</select>
      <input name="quantity" type="number" min="1" value="1" aria-label="So luong bot">
      <button class="secondary" type="submit">Bot</button>
    </form>
  `;
}

function actionButton(order, requiredStatus, label, action) {
  if (order.status !== requiredStatus) {
    return "";
  }

  return `<button data-order-id="${order.id}" data-action="${action}">${label}</button>`;
}

async function changeItem(event, route) {
  event.preventDefault();
  const form = event.currentTarget;
  await postJson(`/api/orders/${form.dataset.orderId}/${route}`, {
    menuCode: form.elements.menuCode.value,
    quantity: Number(form.elements.quantity.value)
  });
  await load();
}

async function runAction(orderId, action) {
  const body = action === "checkout" ? { method: "Card" } : {};
  await postJson(`/api/orders/${orderId}/${action}`, body);
  await load();
}

async function postJson(url, body) {
  const response = await fetch(url, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(body)
  });

  if (!response.ok) {
    const problem = await response.json().catch(() => ({ error: "Thao tac that bai." }));
    showToast(problem.error || "Thao tac that bai.");
    return;
  }

  showToast("Da cap nhat.");
}

function renderSummary() {
  document.getElementById("servedGuests").textContent = state.summary.servedGuests;
  document.getElementById("ordersClosed").textContent = state.summary.ordersClosed;
  document.getElementById("revenue").textContent = money.format(state.summary.revenue);
}

function showToast(message) {
  const toast = document.getElementById("toast");
  toast.textContent = message;
  toast.classList.add("show");
  window.clearTimeout(showToast.timeoutId);
  showToast.timeoutId = window.setTimeout(() => toast.classList.remove("show"), 2600);
}

function shortId(id) {
  return id.slice(0, 8);
}

load().catch(error => showToast(error.message));
