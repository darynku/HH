<template>
  <section class="bg-gray-50 dark:bg-gray-900">
    <Notification :message="errorMessage" :visible="showError" />

    <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
      <div
        class="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Создание вакансии
          </h1>
          <form @submit.prevent="createVacancy">
            <div>
              <label for="title" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Название</label>
              <input v-model="form.title" type="text" id="title"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5" required>
            </div>
            <div>
              <label for="description"
                class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Описание</label>
              <textarea v-model="form.description" id="description"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5"
                required></textarea>
            </div>
            <div>
              <label for="minSalary" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Минимальная
                зарплата</label>
              <input v-model="form.minSalary" type="number" id="minSalary"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5" required>
            </div>
            <div>
              <label for="maxSalary" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Максимальная
                зарплата</label>
              <input v-model="form.maxSalary" type="number" id="maxSalary"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5" required>
            </div>
            <div>
              <label for="region" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Регион</label>
              <input v-model="form.region" type="text" id="region"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5" required>
            </div>
            <div>
              <label for="position"
                class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Должность</label>
              <input v-model="form.position" type="text" id="position"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5" required>
            </div>
            <div>
              <label for="workExperience" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Опыт
                работы (в годах)</label>
              <input v-model="form.workExperience" type="number" id="workExperience"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5" required>
            </div>
            <div>
              <label for="expirationDate" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Срок
                истечения</label>
              <input v-model="form.expirationDate" type="date" id="expirationDate"
                class="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg block w-full p-2.5" required>
            </div>
            <button type="submit"
              class="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center">Создать
              вакансию</button>
          </form>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
import axios from 'axios';
import { ref } from 'vue';
import { jwtDecode } from 'jwt-decode'; // Импортируем jwt-decode
import Notification from '../../User/Notification.vue';

export default {
  name: 'CreateVacancy',
  components: {
    Notification
  },
  setup() {
    const form = ref({
      title: '',
      description: '',
      minSalary: '',
      maxSalary: '',
      region: '',
      position: '', // Исправлено на position
      workExperience: '',
      expirationDate: ''
    });

    const errorMessage = ref('');
    const showError = ref(false);

    // Функция для получения userId из токена
    const getUserIdFromToken = () => {
      const token = localStorage.getItem('jwt');
      console.log(token); // Извлекаем токен из localStorage
      if (!token) {
        return null;
      }
      try {
        const decodedToken = jwtDecode(token);
        console.log(decodedToken); // Расшифровываем токен
        return decodedToken.id; // Возвращаем userId из токена
      } catch (error) {
        console.error('Ошибка при расшифровке токена:', error);
        return null;
      }
    };

    const createVacancy = async () => {
      const userId = getUserIdFromToken();
      if (!userId) {
        errorMessage.value = 'Пользователь не найден';
        showError.value = true;
        return;
      }

      try {
        const postedDate = new Date(); // Current date in local time
        const expirationDate = new Date(form.value.expirationDate); // User input date
        const utcExpirationDate = new Date(expirationDate.getTime() + expirationDate.getTimezoneOffset() * 60000); // Convert to UTC

        const response = await axios.post('https://localhost:5001/api/Vacancy/create', {
          title: form.value.title,
          description: form.value.description,
          minSalary: form.value.minSalary,
          maxSalary: form.value.maxSalary,
          postedDate: postedDate.toISOString(), // Convert to ISO string
          userId: userId,
          region: form.value.region,
          position: form.value.position,
          workExperience: form.value.workExperience,
          expirationDate: utcExpirationDate.toISOString() // Convert to ISO string
        }, {
          headers: {
            Authorization: `Bearer ${localStorage.getItem('jwt')}` // Добавляем токен в заголовок
          }
        });
        console.log('Вакансия создана:', response.data);
        // Очистка формы или перенаправление пользователя
      } catch (error) {
        errorMessage.value = error.response?.data || 'Ошибка при создании вакансии';
        showError.value = true;
      }
    };
  }
}
</script>

<style scoped>
/* Ваши стили */
</style>
