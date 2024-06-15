<script setup>
    import { ref } from "vue";
    import axios from "axios";
    import state from "@/state";
    import { useRouter } from "vue-router";

    const username = ref("");
    const password = ref("");
    const message = ref("");

    const router = useRouter();

    async function login() {
        try {
            const result = await axios.post("http://localhost:8888/api/auth/token", {
                username: username.value,
                password: password.value
            });
            state.token = result.data.token;
            router.push("/");
        } catch {
            message.value = "Wrong username or password";
        }
    }

</script>

<template>
    <div class="w-96 mx-auto">
        <h3>Login</h3>
        <form novalidate @submit.prevent="login">
            <div v-if="message">{{ message }}</div>
            <label for="username">Username</label>
            <input type="text" id="username" v-model="username" />
            <label for="password">Password</label>
            <input type="password" id="password" v-model="password" />
            <button type="submit">Login</button>
        </form>
    </div>
</template>