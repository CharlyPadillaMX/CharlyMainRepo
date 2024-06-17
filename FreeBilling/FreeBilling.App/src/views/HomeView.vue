<script setup>
    import { computed, onMounted, reactive, ref, watch } from "vue";
    import { formatMoney } from "@/formatters";
    import axios from "axios";
    import WaitCursor from "@/components/WaitCursor.vue";
    import state from "@/state";

    const name = ref("Carlos");

    const nancy = ref("Nancy Smith");

    const isBusy = ref(false);
    const customerId = ref(0);

    onMounted(async () => {
        try {
            isBusy.value = true;
            await state.loadCustomers();            
        }
        catch {
            console.log("Failed");
        } finally {
            setTimeout(() => isBusy.value = false, 1000);
        }
    });

    const total = computed(() => {
        //return bills.length;
        return state.timeBills.map(b => b.billingRate * b.hours)
                    .reduce((b, t) => t + b, 0);
    });

    watch(customerId, async () => {
        try {
            isBusy.value = true;
            await state.loadTimeBills(customerId.value);

        } catch (e) {
                console.log(e);
        } finally {
            setTimeout(() => isBusy.value = false, 1000);
        }
    });

    function changeMe () {
        //name.value = "Mustafa";
        name.value += "+";
        //alert(name);
    }

    function newItem() {
        state.timeBills.push({
            "customerId": 1,
            "employeeId": 1,
            "hoursWorked": 5.0,
            "Rate": 114,
            "work": "More work",
            "date": "2023-06-14"
        });

        console.log(state.timeBills.length);
    }
</script>

<template>
    <header class="flex text-red-900">
        <h3>Our App</h3>
    </header>

    <main>
        <h1>Hello from Vue</h1>
        <WaitCursor :busy="isBusy" msg="Te esperas..."></WaitCursor>
        <!--<div>{{ name.toUpperCase() }}</div>
        <button class="btn" @click="changeMe">Change Me</button>
        <img src="http://localhost:8888/imgs/nancy.jpg" :alt="nancy" :title="nancy.toUpperCase()" />
        <button class="btn" @click="newItem">New Item</button>-->
        <div>
            <label>Customers:</label>
            <select class="w-96 mx-2" v-model="customerId">
                <option v-for="c in state.customers" :value="c.id">{{ c.companyName }}</option>
            </select>
        </div>
        <table class="w-full">
            <thead>
                <tr class="text-bold bg-blue-900 text-white">
                    <td>Hours</td>
                    <td>Date</td>
                    <td>Description</td>
                    <td>Rate</td>
                    <td>Employee</td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="b in state.timeBills">
                    <td>{{ b.hours }}</td>
                    <td>{{ b.date }}</td>
                    <td>{{ b.workPerformed }}</td>
                    <td>{{ b.billingRate }}</td>
                    <td>{{ b.employee.name }}</td>
                </tr>
            </tbody>
        </table>
        <div>Total: {{ formatMoney(total) }}</div>
    </main>
</template>

