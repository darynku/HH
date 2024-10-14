import * as signalR from "@microsoft/signalr";

// Инициализируем соединение с SignalR и передаем токен из localStorage
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/chat", {
        accessTokenFactory: () => localStorage.getItem('jwt')  // Токен из localStorage
    })
    .withAutomaticReconnect()
    .build();

// Подключаем обработчик для получения сообщений
connection.on("ReceiveMessage", (message) => {
    console.log(`Message from ${message.userName} at ${message.createdDate}: ${message.messageText}`);
    // Добавьте логику для отображения сообщения на странице
});

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);  // Попытка переподключения
    }
}

connection.onclose(async () => {
    await start();
});

start();

export default connection;
