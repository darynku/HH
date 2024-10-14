<template>
    <div>
      <input v-model="message" placeholder="Type your message" />
      <button @click="sendMessage">Send</button>
    </div>
  </template>
  
  <script>
  import signalrService from './signalrService.js';
  import { jwtDecode } from 'jwt-decode';  // Правильный импорт jwt-decode
  
  export default {
    data() {
      return {
        message: ''  // Сообщение
      };
    },
    methods: {
      sendMessage() {
        // Получаем токен и декодируем его
        const token = localStorage.getItem('jwt');
        const decodedToken = jwtDecode(token);
  
        // Имя пользователя извлекаем из токена (поле name)
        const userName = decodedToken.userName;
  
        // Отправляем имя пользователя и сообщение на сервер через SignalR
        signalrService.invoke("SendMessage", userName, this.message)
          .catch(err => console.error(err));
  
        // Очищаем поле ввода после отправки сообщения
        this.message = "";
      }
    }
  };
  </script>
  