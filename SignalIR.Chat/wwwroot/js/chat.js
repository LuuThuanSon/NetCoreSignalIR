"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/hubs/clock")
    //.withAutomaticReconnect({
    //    nextRetryDelayInMilliseconds: retryContext => {
    //        if (retryContext.elapsedMilliseconds < 60000) {
    //            // If we've been reconnecting for less than 60 seconds so far,
    //            // wait between 0 and 10 seconds before the next reconnect attempt.
    //            return Math.random() * 10000;
    //        } else {
    //            // If we've been reconnecting for more than 60 seconds so far, stop reconnecting.
    //            return null;
    //        }
    //    }
    //})
    .configureLogging(signalR.LogLevel.Information)
    .build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("ShowTime", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = "Time Zone: " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

async function start() {
    try {
        await connection.start().then(function () {
            console.assert(connection.state === signalR.HubConnectionState.Connected);
            document.getElementById("sendButton").disabled = false;
            console.log("connected");
        }).catch(function (err) {
            return console.error(err.toString());
        });
    } catch (err) {
        console.assert(connection.state === signalR.HubConnectionState.Disconnected);
        console.log(err);
        document.getElementById("messageInput").disabled = true;
        const li = document.createElement("li");
        li.textContent = `Connection lost due to error "${error}". Reconnecting.`;
        document.getElementById("messagesList").appendChild(li);
        //setTimeout(() => start(), 5000);
    }
};

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.onclose(async () => {
    await start();
});

start();

//connection.onreconnecting(error => {
//    console.assert(connection.state === signalR.HubConnectionState.Reconnecting);

//    document.getElementById("messageInput").disabled = true;

//    const li = document.createElement("li");
//    li.textContent = `Connection lost due to error "${error}". Reconnecting.`;
//    document.getElementById("messagesList").appendChild(li);
//});

//connection.onclose(error => {
//    console.assert(connection.state === signalR.HubConnectionState.Disconnected);

//    document.getElementById("messageInput").disabled = true;

//    const li = document.createElement("li");
//    li.textContent = `Connection closed due to error "${error}". Try refreshing this page to restart the connection.`;
//    document.getElementById("messagesList").appendChild(li);
//});