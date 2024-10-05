<template>
  <section class="bg-gray-50 dark:bg-gray-900">
    <Notification :message="errorMessage" :visible="showError" />

    <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
      <div class="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
        <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
          <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
            Просмотр вакансии
          </h1>

          <div v-if="vacancy">
            <p><strong>Название:</strong> {{ vacancy.title }}</p>
            <p><strong>Описание:</strong> {{ vacancy.description }}</p>
            <p><strong>Должность:</strong> {{ vacancy.position }}</p>
            <p><strong>Зарплата:</strong> {{ vacancy.salary.minSalary }} - {{ vacancy.salary.maxSalary }}</p>
            <p><strong>Опыт работы:</strong> {{ vacancy.workExperience }} лет</p>
            <p><strong>Регион:</strong> {{ vacancy.region }}</p>
            <p><strong>Просмотры:</strong> {{ vacancy.views }}</p>
            <p><strong>Дата публикации:</strong> {{ new Date(vacancy.postedDate).toLocaleDateString() }}</p>
            <p><strong>Количество заявок:</strong> {{ vacancy.applicationsCount }}</p>

            <div v-if="vacancy.files && vacancy.files.length > 0">
              <h3>Файлы:</h3>
              <ul>
                <li v-for="file in vacancy.files" :key="file.pathToStorage">
                  <img :src="file.pathToStorage" alt="Файл" class="w-32 h-32 object-cover" />
                </li>
              </ul>
            </div>
          </div>
          
          <div v-else>
            <p>Загрузка данных...</p>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script>
export default {
  data() {
    return {
      vacancy: null,
      errorMessage: '',
      showError: false,
    };
  },
  created() {
    this.fetchVacancyDetails();
  },
  methods: {
    async fetchVacancyDetails() {
      try {
        const vacancyId = this.$route.params.vacancyId;
        const response = await fetch(`https://localhost:5001/api/Vacancy/${vacancyId}`);
        if (!response.ok) {
          throw new Error('Не удалось загрузить данные о вакансии');
        }
        this.vacancy = await response.json();
      } catch (error) {
        this.errorMessage = error.message;
        this.showError = true;
      }
    },
  }
}
</script>

<style scoped>
/* Добавьте стили по необходимости */
</style>
