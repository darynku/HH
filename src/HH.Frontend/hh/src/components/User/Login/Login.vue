<template>
  <section class="bg-gray-50 dark:bg-gray-900">
    <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
      <a href="#" class="flex items-center mb-6 text-2xl font-semibold text-gray-900 dark:text-white">
        <img class="w-8 h-8 mr-2" src="https://flowbite.s3.amazonaws.com/blocks/marketing-ui/logo.svg" alt="logo">
        Вход в аккаунт
      </a>
      <div
        class="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Войти в ваш аккаунт
          </h1>
          <form @submit.prevent="login">
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
            <button type="submit"
              class="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800">Войти</button>
            <p class="text-sm font-light text-gray-500 dark:text-gray-400">
              Нет аккаунта? <a href="/register"
                class="font-medium text-primary-600 hover:underline dark:text-primary-500">Зарегистрируйтесь здесь</a>
            </p>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import axios from "axios";
import { ref } from "vue";
import { useRouter } from "vue-router";

export default {
  name: "Login",
  setup() {
    const form = ref({
      email: "",
      password: "",
    });

    const router = useRouter();

    // Добавляем interceptor для axios
    axios.interceptors.request.use((config) => {
      const token = localStorage.getItem("token");
      if (token) {
        // Добавляем токен в заголовки запроса
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    });

    const login = async () => {
      try {
        const response = await axios.post("https://localhost:5001/api/User/login", {
          email: form.value.email,
          password: form.value.password,
        });

        const token = response.data.value.token;

        // Сохраняем токен в localStorage
        localStorage.setItem("jwt", token);

        // Перенаправление после успешного входа
        await router.push("/vacancies/all");
      } catch (error) {
        console.error("Ошибка входа:", error.response ? error.response.data : error.message);
      }
    };

    return {
      form,
      login,
    };
  },
};
</script>

<style scoped>
.login-container {
  width: 300px;
  margin: 100px auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.form-group {
  margin-bottom: 15px;
}

.error {
  color: red;
  margin-top: 10px;
}
</style>
