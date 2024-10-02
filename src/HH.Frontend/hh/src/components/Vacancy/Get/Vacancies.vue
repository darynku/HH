<template>
  <section class="bg-gray-50 dark:bg-gray-900 min-h-screen">
    <div class="container mx-auto py-8">
      <h1 class="text-3xl font-bold text-gray-900 dark:text-white mb-6">Вакансии</h1>

      <!-- Отображение ошибки -->
      <div v-if="errorMessage" class="text-center text-red-500">{{ errorMessage }}</div>

      <!-- Загрузка данных -->
      <div v-if="loading && vacancies.length === 0" class="text-center text-gray-500 dark:text-gray-400">Загрузка...
      </div>

      <!-- Если вакансий нет -->
      <div v-if="vacancies.length === 0 && !loading" class="text-center text-gray-500 dark:text-gray-400">Нет доступных
        вакансий</div>

      <!-- Список вакансий в вертикальный ряд -->
      <div class="flex flex-col space-y-6">
        <div v-for="vacancy in vacancies" :key="vacancy.id" class="bg-white dark:bg-gray-800 p-6 rounded-lg shadow-md">
          <h2 class="text-xl font-semibold text-gray-900 dark:text-white">Название: {{ vacancy.title }}</h2>
          <p class="text-gray-700 dark:text-gray-300">Описаниие: {{ vacancy.description }}</p>
          <p class="text-gray-900 dark:text-white mt-2">Зарплата: {{ vacancy.salary }} ₽</p>
        </div>
      </div>

      <!-- Кнопка "Показать еще" -->
      <div class="flex justify-center mt-8" v-if="hasMore">
        <button @click="loadMore" class="bg-blue-500 text-white py-2 px-4 rounded-lg">Показать еще</button>
      </div>
    </div>
  </section>
</template>

<script>
import axios from 'axios';
import { ref, onMounted } from 'vue';

export default {
  name: 'VacanciesPage',
  setup() {
    const vacancies = ref([]);
    const loading = ref(false);
    const errorMessage = ref('');
    const page = ref(1);
    const pageSize = ref(10);
    const hasMore = ref(true);

    const fetchVacancies = async () => {
      loading.value = true;
      errorMessage.value = '';

      // Получаем токен из localStorage
      const token = localStorage.getItem('jwt');

      if (!token) {
        errorMessage.value = 'Токен не найден. Пожалуйста, авторизуйтесь.';
        loading.value = false;
        return;
      }

      try {
        const response = await axios.get(`https://localhost:5001/api/Vacancy/getAll?page=${page.value}&pageSize=${pageSize.value}`, {
          headers: {
            Authorization: `Bearer ${token}` // Добавляем токен в заголовки
          }
        });

        // Если данных меньше, чем запрашивалось - больше данных нет
        if (response.data.length < pageSize.value) {
          hasMore.value = false;
        }

        // Добавляем вакансии к текущему списку
        vacancies.value = [...vacancies.value, ...response.data];
      } catch (error) {
        if (error.response && error.response.status === 401) {
          errorMessage.value = 'Ошибка авторизации. Пожалуйста, войдите в систему.';
        } else {
          errorMessage.value = 'Ошибка при загрузке вакансий.';
        }
      } finally {
        loading.value = false;
      }
    };

    // При загрузке компонента подгружаем первую страницу
    onMounted(() => {
      fetchVacancies();
    });

    const loadMore = () => {
      page.value++;
      fetchVacancies();
    };

    return {
      vacancies,
      loading,
      errorMessage,
      page,
      loadMore,
      hasMore
    };
  }
};
</script>

<style scoped>
/* Примерные стили */
</style>
