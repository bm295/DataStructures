/*
  Insert Node at the end of a linked list 
  head pointer input could be NULL as well for empty list
  Node is defined as 
  struct Node
  {
     int data;
     struct Node *next;
  }
*/
Node* Insert(Node *head,int data)
{
  // Complete this method
    Node *temp = head;    
    Node *newNode = (Node *)malloc(sizeof(Node));
    newNode -> data = data;
    newNode -> next = NULL;
    
    if (temp == NULL) {
        head = newNode;
        return head;
    }
        
    while(temp -> next != NULL)
        temp = temp -> next;
    temp -> next = newNode;
    return head;
}
