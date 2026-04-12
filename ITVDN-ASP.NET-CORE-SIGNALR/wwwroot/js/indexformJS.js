const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

document.getElementById("sendBtn").addEventListener("click", () => {
    const username = document.getElementById("username").value;
    const message = document.getElementById("message").value;

    hubConnection.invoke("SendMessage", username, message)
        .catch(error => console.error(error.toString()));
});

document.getElementById("notifyBtn").addEventListener("click", () => {
    fetch("/api/notify/broadcast", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            username: "Server",
            message: "Hello from API"
        })
    })
        .then(res => res.text())
        .then(data => console.log(data));
});

hubConnection.on("ReceiveMessage", (username, message) => {
    const userNameElem = document.createElement("b");
    userNameElem.textContent = `${username}: `;

    const elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    const firstElem = document.getElementById("messages").firstChild;
    document.getElementById("messages").insertBefore(elem, firstElem);
});

hubConnection.on("UserConnected", (id) => console.log("Connected: ", id));

hubConnection.on("UserDisconnected", (id) => console.log("Disconnected: ", id));

hubConnection.start()
    .then(() => console.log("Connection to ChatHub"))
    .catch(error => console.error(error.toString()));