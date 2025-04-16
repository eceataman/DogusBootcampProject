document.addEventListener("DOMContentLoaded", function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .build();

    connection.start().catch(err => console.error(err.toString()));

    document.getElementById("sendBtn").addEventListener("click", function (e) {
        const message = document.getElementById("chatInput").value;
        const user = document.querySelector("a.nav-link.disabled")?.innerText || "Ziyaretçi";

        if (message.trim() === "") return;

        connection.invoke("SendMessage", user, message).catch(err => console.error(err.toString()));
        document.getElementById("chatInput").value = "";
        e.preventDefault();
    });

    connection.on("ReceiveMessage", function (user, message) {
        const chatMessages = document.getElementById("chatMessages");
        const msgElement = document.createElement("div");
        msgElement.innerHTML = `<strong>${user}:</strong> ${message}`;
        chatMessages.appendChild(msgElement);
        chatMessages.scrollTop = chatMessages.scrollHeight;
    });
});
