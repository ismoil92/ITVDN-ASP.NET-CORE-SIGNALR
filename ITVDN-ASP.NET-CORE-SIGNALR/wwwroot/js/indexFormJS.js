const hubConnectionBuilder = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .build();

document.getElementById("sendBtn").addEventListener("click", () => {
    const message = document.getElementById("message").value;
    const username = document.getElementById("username").value
    hubConnectionBuilder.invoke("Send", message, username)
        .catch(error => console.error(error.toString()));
});

hubConnectionBuilder.on("Receive", (message, username) => {
    const userNameElem = document.createElement("b");
    userNameElem.textContent = `${username}: `;

    const elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    const firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);
});

hubConnectionBuilder.start()
    .then(() => document.getElementById("sendBtn").disabled = false)
    .catch(error => console.error(error.toString()));