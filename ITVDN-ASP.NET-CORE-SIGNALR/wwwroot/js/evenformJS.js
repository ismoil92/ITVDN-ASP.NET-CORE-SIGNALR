const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/even")
    .build();

document.getElementById("sendBtn").addEventListener("click", () => {
    const message = document.getElementById("message").value;

    hubConnection.invoke("Send", message)
        .catch(error => console.error(error.toString()));
});

hubConnection.on("Receive", (message) => {
    const messageElem = document.createElement("p");
    messageElem.textContent = message;
    document.getElementById("chatroom").appendChild(messageElem);
});

hubConnection.start()
    .then(() => console.log("Connection to Even Chat Hub"))
    .catch(error => console.error(error.toString()));