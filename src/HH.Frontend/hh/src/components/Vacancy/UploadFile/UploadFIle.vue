<template>
    <section class="bg-gray-50 dark:bg-gray-900">
      <div class="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
        <div class="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
          <div class="p-6 space-y-4 md:space-y-6 sm:p-8">
            <h1 class="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
              Загрузить файлы
            </h1>
            <form @submit.prevent="uploadFiles">
              <div>
                <label for="files" class="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Файлы</label>
                <input @change="handleFileChange" type="file" id="files" class="block w-full text-sm text-gray-900 bg-gray-50 rounded-lg border border-gray-300 cursor-pointer dark:text-gray-400 focus:outline-none dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400" multiple>
              </div>
              <button type="submit" class="w-full text-white bg-primary-600 hover:bg-primary-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center">
                Загрузить файлы
              </button>
            </form>
          </div>
        </div>
      </div>
    </section>
  </template>
  
  <script>
  import axios from 'axios';
  import { ref } from 'vue';
  
  export default {
    name: 'UploadFiles',
    setup() {
      const selectedFiles = ref([]);
  
      const handleFileChange = (event) => {
        selectedFiles.value = Array.from(event.target.files); // Преобразуем файлы в массив
      };
  
      const uploadFiles = async () => {
        if (!selectedFiles.value.length) {
          alert('Пожалуйста, выберите файлы для загрузки.');
          return;
        }
  
        const formData = new FormData();
        selectedFiles.value.forEach((file) => {
          formData.append('files', file); // Добавляем файлы в FormData
        });
  
        try {
          const vacancyId = 'ваш-vacancyId'; // Замените на реальный VacancyId
          const response = await axios.post(`https://localhost:5001/api/vacancy/${vacancyId}/file`, formData, {
            headers: {
              'Content-Type': 'multipart/form-data',
              Authorization: `Bearer ${localStorage.getItem('jwt')}`, // Добавляем токен в заголовок
            },
          });
  
          console.log('Файлы успешно загружены:', response.data);
        } catch (error) {
          console.error('Ошибка при загрузке файлов:', error.response?.data || error.message);
        }
      };
  
      return {
        handleFileChange,
        uploadFiles,
      };
    },
  };
  </script>
  
  <style scoped>
  /* Ваши стили */
  </style>
  