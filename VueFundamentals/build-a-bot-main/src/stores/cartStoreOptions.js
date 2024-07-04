import { defineStore } from "pinia";

export const useCartStore = defineStore('cart', {
    state: () => ({
      cart: [],
    }),
    getters: {
      cartTotal: (state) => state.cart.reduce((prev, cur) => prev += cur.cost, 0),
    },
});