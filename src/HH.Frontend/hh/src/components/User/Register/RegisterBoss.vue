<template>
  <section class="bg-gray-50 dark:bg-gray-900">
    <!-- Notification component -->
    <Notification :message="errorMessage" :visible="showError" />

    <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
      <a href="#" class="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-white">
        <img class="w-8 h-8 mr-2" src="https://flowbite.s3.amazonaws.com/blocks/marketing-ui/logo.svg" alt="logo">
        Регистрация работодателя
      </a>
      <div
        class="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Регистрация работодателя
          </h1>
          <form @submit.prevent="registerBoss">
            <div>
              <label for="email" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Ваш email</label>
              <input v-model="form.email" type="email" id="email"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                placeholder="name@company.com" required>
            </div>
            <div>
              <label for="password" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Пароль</label>
              <input v-model="form.password" type="password" id="password" placeholder="••••••••"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required>
            </div>
            <div>
              <label for="username" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Имя
                пользователя</label>
              <input v-model="form.username" type="text" id="username" placeholder="Имя пользователя"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                required>
            </div>
            <button type="submit"
              class="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">Создать
              аккаунт</button>
            <p class="text-sm font-light text-gray-500 dark:text-gray-400">
              Уже есть аккаунт? <a href="/login"
                class="font-medium text-primary-600 hover:underline dark:text-primary-500">Войти здесь</a>
            </p>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import axios from 'axios';
import { ref, watch } from 'vue';
import Notification from '../Notification.vue'; // Убедитесь, что путь правильный

export default {
  name: 'RegisterBoss',
  components: {
    Notification
  },
  setup() {
    const form = ref({
      email: '',
      password: '',
      username: ''
    });
    const errorMessage = ref('');
    const showError = ref(false);

    let errorTimeout = null; // Хранение таймера для сброса ошибки

    const registerBoss = async () => {
      try {
        const response = await axios.post('https://localhost:51390/api/User/registerBoss', {
          userName: form.value.username,
          email: form.value.email,
          password: form.value.password
        });
        console.log('Успешная регистрация:', response.data);
        // Очистить форму или перенаправить пользователя
        form.value.email = '';
        form.value.password = '';
        form.value.username = '';
        showError.value = false; // Скрыть уведомление при успешной регистрации
      } catch (error) {
        if (error.response) {
          if (error.response.status === 401) {
            errorMessage.value = 'Неправильные учетные данные или email уже занят.';
          } else {
            errorMessage.value = error.response.data || 'Ошибка при регистрации.';
          }
        } else {
          errorMessage.value = 'Ошибка при регистрации: ' + error.message;
        }
        showError.value = true; // Показывать уведомление при ошибке

        // Очистить предыдущий таймер, если он существует
        if (errorTimeout) {
          clearTimeout(errorTimeout);
        }

        // Установить новый таймер на 3 секунды
        errorTimeout = setTimeout(() => {
          showError.value = false; // Скрыть уведомление после 3 секунд
        }, 3000);
      }
    };

    return {
      form,
      registerBoss,
      errorMessage,
      showError
    };
  }
}
</script>

<style scoped>
/* Ваши стили */
</style>
