let username = "";
let groupname = "";

const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7204/chat")
    .build();

    // button for enter to group
document.getElementById("entergroupBtn").addEventListener("click", () => {
    username = document.getElementById("username").value;
    groupname = document.getElementById("groupname").value;

    hubConnection.invoke("EnterToGroup", username, groupname)
        .catch(error => console.error(error.toString()));
});

// button for exit from group
document.getElementById("exitgroupBtn").addEventListener("click", () => {
    username = document.getElementById("username").value;
    groupname = document.getElementById("groupname").value;

    hubConnection.invoke("ExitFromGroup", username, groupname)
        .catch(error => console.error(error.toString()));
});

// button for send message all users in group
document.getElementById("sendBtn").addEventListener("click", () => {
    const message = document.getElementById("message").value;

    hubConnection.invoke("SendMessage", message, username, groupname)
        .catch(error => console.error(error.toString()));
});

//button for send message all users but except user by connection Id
document.getElementById("sendExpBtn").addEventListener("click", () => {
    message = document.getElementById("messageExp").value;
    groupname = document.getElementById("groupnameExp").value;
    hubConnection.invoke("SendExcept", message, groupname)
        .catch(error => console.error(error.toString()));
});

// method SendExcept in class ChatHub
hubConnection.on("ReceiveMessage", (message) => {
    const elem = document.createElement("p");

    elem.textContent = message;

    const firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});

// method SendMessage in class ChatHub
hubConnection.on("Receive", (message, username) => {
    const userNameElem = document.createElement("b");
    userNameElem.textContent = `${username}: `;

    const elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    const firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});

// methods EnterToGroup and ExitFromGroup in class ChatHub
hubConnection.on("Notify", (message) => {
    const elem = document.createElement("p");

    elem.textContent = message;

    const firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});


hubConnection.start()
    .then(() => console.log("Chat hub connection"))
    .catch(error => console.error(error.toString()));