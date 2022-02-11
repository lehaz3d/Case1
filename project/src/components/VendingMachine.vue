<template>
<label class="lbStyle">Выберите машину:</label>
  <button class="buttonStyle"
    v-for="vendingMachine in vendingMachines"
    :key="vendingMachine.id"
    @click="$router.push(`/${vendingMachine.id}`)"
  >
    {{ vendingMachine.id }}
  </button>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      vendingMachines: [],
    };
  },

  methods: {
    async getVendingMachines() {
      this.vendingMachines = (
        await axios.get("https://192.168.0.77/api/VendingMachines")
      ).data;
    },
  },

  created() {
      this.getVendingMachines();
  }
};
</script>

<style>
.buttonStyle {
  width: 100px;
  height: 100px;
  margin: 10px;
}

.lbStyle {
  font-size: 35px;
}
</style>