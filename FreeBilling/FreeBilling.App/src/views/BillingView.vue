<script setup>
    import { onMounted, reactive, ref } from "vue";
    import axios from "axios";
    import state from "@/state";
    import { useRouter } from "vue-router";

    const router = useRouter();
    const bill = ref({});
    const employees = reactive([]);
    const customers = reactive([]);
    const message = ref("");

    onMounted(async () => {
        try {
            if (!state.token) {
                router.push("/login");
            }

            const employeesResult = await axios.get("http://localhost:8888/api/employees", {
                headers: {
                    "authorization": `Bearer ${state.token}`
                }
            });
            employees.splice(0, employees.length, ...employeesResult.data);

            const customersResult = await axios.get("http://localhost:8888/api/customers", {
                headers: {
                    "authorization": `Bearer ${state.token}`
                }
            });
            customers.splice(0, customers.length, ...customersResult.data);

        } catch (e) {
            message.value = e;
        }
    });

    async function saveBill() {
        //TODO should do validation
        try {

            const result = await axios.post("http://localhost:8888/api/timebills", bill.value, {
                headers: {
                    "authorization": `Bearer ${state.token}`
                }
            });

            router.push("/");

        } catch (e) {
            message.value = e;
        }
    }

</script>

<template>
    <div class="w-96 mx-auto bg-white p-2">
        <h1>Billing</h1>
        <div v-if="message">{{ message }}</div>
        <form novalidate @submit.prevent="saveBill">
            <label for="date">Date</label>
            <input type="date" name="date" id="date" v-model="bill.date" />
            <label for="time">Hours Worked</label>
            <input type="text" name="time" id="time" v-model="bill.hoursWorked" />
            <label for="workPerformed">Work Performed</label>
            <textarea rows="4" name="workPerformed" id="workPerformed" v-model="bill.work"></textarea>
            <label for="employee">Employee</label>
            <select id="employee" name="employee" v-model="bill.employeeId">
                <option v-for="e in employees" :key="e.id" :value="e.id">{{ e.name }}</option>
            </select>
            <label for="rate">Rate</label>
            <input type="number" id="rate" v-model="bill.rate" />
            <label for="client">Client</label>
            <select id="client" name="client" v-model="bill.customerId">
                <option v-for="c in customers" :key="c.id" :value="c.id">{{ c.companyName }}</option>
            </select>
            <div>
                <button type="submit" class="bg-green-600 hover:bg-green-500 mr-2 ">Save</button>
                <button>Cancel</button>
            </div>
        </form>
        <pre>{{ bill }}</pre>
    </div>
</template>