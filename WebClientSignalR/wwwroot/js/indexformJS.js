const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7204/chat", {
    })
    .withAutomaticReconnect([0, 1000, 2000, 3000, 4000, 5000]) // task number 2
    .build();


document.getElementById("sendBtn").addEventListener("click", () => {
    const username = document.getElementById("username").value;
    const message = document.getElementById("message").value;

    hubConnection.invoke("SendMessage", username, message)
        .catch(error => console.log(error.toString()));
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

hubConnection.on("OnConnected", (id) => console.log("Connected: ", id));

hubConnection.on("OnDisconnected", (id) => confirm.log("Disconnected: ", id));

hubConnection.start()
    .then(() => console.log("Connection ChatHub"))
    .catch(error => console.error(error.toString()));

async function getTime() {
    const time = await hubConnection.invoke("GetServerTime");
    alert("Server time: " + time);
}

async function getTimeAsync() {
    const time = await hubConnection.invoke("GetServerTimeAsync");
    alert("Server time async: " + time);
}

//task number 3
//function createCustomReconnectPolicy() {
//    let attempt = 0;
//    let totalTime = 0;

//    return
//    {
//        nextRetryDelayMilliseconds: () => {
//            if (totalTime >= 60000) return null;

//            let delay;

//            if (attempt === 0) {
//                delay = Math.floor(Math.random() * 10000) + 1000; // 1-10 sec
//            }
//            else {
//                delay = Math.pow(2, attempt) * 1000;
//            }

//            totalTime += delay;
//            attempt++;

//            return delay;
//        }
//    };
//}