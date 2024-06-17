//state.js

import { reactive } from "vue";
import axios from "axios";
import http from "../http";

export default reactive({
    token: "",
    customers: [],
    employees: [],
    timeBills: [],
    currentCustomerId: null,
    async loadCustomers() {
        if (this.customers.length === 0) {
            const customersResult = await http.get("http://localhost:8888/api/customers");
            this.customers.splice(this.customers, this.customers.length, ...customersResult.data);
        }
    },
    async loadEmployees() {
        if (this.employees.length === 0) {
            const employeesResult = await http.get("http://localhost:8888/api/employees");
            this.employees.splice(this.employees, this.employees.length, ...employeesResult.data);
        }
    },
    async loadTimeBills(customerId) {
        this.currentCustomerId = customerId;
        const result = await http.get(`http://localhost:8888/api/customers/${this.currentCustomerId}/timebills`);

        if (result.status === 200) {
            this.timeBills.splice(this.timeBills, this.timeBills.length, ...result.data);
        }
    },
    async saveBill(bill) {
        const result = await http.post("http://localhost:8888/api/timebills", bill);
        if (result.status === 201) {
            if (result.data.customerId === this.currentCustomerId) {
                this.timeBills.push(result.data);
            }
        }
    }
});