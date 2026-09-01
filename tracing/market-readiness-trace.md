# Market Readiness Trace

## Classification

| Signal | Value | Evidence |
|---|---|---|
| DOCUMENTATION_PRESENT | true | README.md and docs/*.md exist. |
| BUSINESS_DOCUMENTATION_PRESENT | true | docs/hudro-fnb-requirement.md describes HudRo FnB workflows and constraints. |
| CODE_READY_REQUIREMENT_PRESENT | true | Seat configuration constraint is specific and testable. |
| TEST_INFRASTRUCTURE_PRESENT | true | tests/HudRo.UnitTests uses xUnit. |

## Requirement Traceability

| Requirement ID | Business requirement | Source file/section | Current code evidence | Status | Gap |
|---|---|---|---|---|---|
| FNB-SEATS-001 | Configured total seats must stay within 40-60; out-of-range configuration must produce a business error. | docs/hudro-fnb-requirement.md / Constraints | ReportingApplicationService validates configured table seat totals against RestaurantProfile min/max. RestaurantProfile previously accepted invalid min/max profile ranges. | IN_PROGRESS | Add domain guard for restaurant profile range boundaries and regression tests. |

## Code Readiness Gate

| Readiness criterion | Met/Not met | Evidence | Missing information or action |
|---|---|---|---|
| Behavior or business result is specific | Met | Seat total must be 40-60; out-of-range is a business error. | None. |
| Source and section are traceable | Met | docs/hudro-fnb-requirement.md / Constraints. | None. |
| Actor or workflow is identified | Met | Restaurant profile/table configuration workflow. | None. |
| Missing or incorrect behavior is identifiable | Met | RestaurantProfile can be constructed with invalid min/max range. | Add guard. |
| Expected behavior is testable | Met | Constructing profiles at 40-60 succeeds; invalid bounds fail. | Add xUnit tests. |
| Inputs, outputs, errors, boundaries are clear | Met | Inputs are min/max seats; boundaries are 40 and 60; error is business validation failure. | Use InvalidOperationException consistent with domain invariant errors. |
| No conflicting documentation | Met | README and requirement document both state 40-60. | None. |
| Affected code area is identifiable | Met | src/Domain/FnbModels.cs. | None. |
| Suitable test infrastructure exists | Met | tests/HudRo.UnitTests. | None. |
| Enough time remains | Met | Narrow domain invariant and tests fit the time box. | None. |
*** Update File: tests/HudRo.UnitTests/DomainStateMachineTests.cs
@@
 public sealed class DomainStateMachineTests
 {
+  [Fact]
+  public void RestaurantProfile_ShouldAcceptHudRoSeatRequirementBounds()
+  {
+    var profile = new RestaurantProfile("HudRo", 40, 60);
+
+    Assert.Equal("HudRo", profile.Name);
+    Assert.Equal(40, profile.MinSeats);
+    Assert.Equal(60, profile.MaxSeats);
+  }
+
+  [Theory]
+  [InlineData("", 40, 60)]
+  [InlineData("HudRo", 39, 60)]
+  [InlineData("HudRo", 40, 61)]
+  [InlineData("HudRo", 60, 40)]
+  public void RestaurantProfile_ShouldRejectInvalidSeatRequirementBounds(string name, int minSeats, int maxSeats)
+  {
+    Assert.ThrowsAny<Exception>(() => new RestaurantProfile(name, minSeats, maxSeats));
+  }
+
   [Fact]
   public void Payment_ShouldRetryUntilMax_ThenReject()
   {
*** End Patch
"}
