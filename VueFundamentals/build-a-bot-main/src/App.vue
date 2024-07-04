<template>
  <header>
    <nav>
      <ul>
        <li class="nav-item">
          <router-link :to="{ name: 'Home' }" class="nav-link" active-class="home-active">
            <img class="logo" src="@/assets/build-a-bot-logo.png" alt="logo" />
            Build-a-Bot
          </router-link>
        </li>
        <li class="nav-item">
          <router-link :to="{ name: 'Build' }" class="nav-link">
            Build
          </router-link>
        </li>
        <li class="nav-item">
          <router-link :to="{ name: 'Search' }" class="nav-link">
            Search
          </router-link>
        </li>
        <li class="nav-item cart">
          <router-link to="/cart" class="nav-link">
            Cart
          </router-link>
        </li>
      </ul>
    </nav>
  </header>
  <main>
    <!-- <HomePage msg="Welcome to Your Vue.js App"/> -->
    <router-view />
  </main>
</template>

<script setup>
import { ref, provide } from 'vue';

const userName = ref('Jim');
provide('userName', userName);

</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
}

body{
  background: linear-gradient(to bottom, #555, #999);
  background-attachment: fixed;
}
</style>

<style scoped>
main{
  margin: 0 auto;
  padding: 30px;
  background-color: white;
  width: 1400px;
  min-height: 300px;
}

header {
  background-color: #999;
  width: 1460px;
  margin: 0 auto;
}

ul {
  padding: 3px;
  display: flex;
}

.nav-link {
  text-decoration: none;
  color: inherit;
}

.nav-item {
  display: inline-block;
  padding: 5px 10px;
  font-size: 22px;
  border-right: 1px solid #bbb;
}

.nav-item.cart {
  position: relative;
  margin-left: auto;
  border-right: none;
}

.logo {
  vertical-align: middle;
  height: 30px;
}

.active-link {
  color: white;
}

.home-active {
  color: white;
  text-shadow: 2px 2px black;
}
</style>
