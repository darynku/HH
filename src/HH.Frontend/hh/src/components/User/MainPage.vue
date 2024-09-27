<template>
  <div class="main-page">
    <h1>Выберите вашу роль</h1>
    
    <div class="options">
      <!-- Блок для Работников -->
      <div class="block boss-block" @click="redirectToWorkerForm">
        <h2>Я Работник</h2>
        <p>Зарегистрироваться как работник</p>
      </div>
      
      <!-- Блок для Работодателей -->
      <div class="block worker-block"  @click="redirectToBossForm">
        <h2>Я Работодатель</h2>
        <p>Зарегистрироваться как работодатель</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  methods: {
    redirectToWorkerForm() {
      this.$router.push({ name: 'worker-register' });
    },
    
    // Перенаправление на форму регистрации Работодателя
    redirectToBossForm() {
      this.$router.push({ name: 'boss-register' });
    }
  }
}
</script>

<style scoped>
.main-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 50px;
}

.options {
  display: flex;
  gap: 30px;
}

.block {
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 10px;
  cursor: pointer;
  transition: all 0.3s ease;
}


.worker-block {
  background-color: #9c3b3b;
}

.worker-block:hover {
  background-color: #ae2a2a;
}

.boss-block {
  background-color: darkblue;
}

.boss-block:hover {
  background-color: rgb(0, 0, 207);
}


h1 {
  margin-bottom: 40px;
}

h2 {
  margin-bottom: 10px;
}
</style>
