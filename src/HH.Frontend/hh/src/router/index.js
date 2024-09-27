import { createRouter, createWebHistory } from 'vue-router';
import MainPage from '../components/User/MainPage.vue';
import RegisterBoss from '../components/User/Register/RegisterBoss.vue';
import RabForm from '../components/User/Register/RabForm.vue';
import Login from '../components/User/Login/Login.vue';
import Vacancies from '../components/Vacancy/Get/Vacancies.vue';
import VacancyCreate from '../components/Vacancy/Create/VacancyCreate.vue';

const routes = [
  { path: '/', name: 'main', component: MainPage },
  { path: '/register/worker', name: 'worker-register', component: RegisterBoss },
  { path: '/register/boss', name: 'boss-register', component: RabForm },
  { path: '/login', name: 'login', component: Login },
  { path: '/vacancies/all', name: 'vacancies', component: Vacancies},
  { path: '/vacancies/create', name: 'vacancies-create', component: VacancyCreate}
];
const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
