<template>
  <div>
    <label>Административная панель</label>
    <div class="container container__vertical" v-for="vendingMachineCoins in vendingMachineCoins" :key="vendingMachineCoins.id">
      <label>{{vendingMachineCoins.coinsId}} - </label>
      <label>{{vendingMachineCoins.count}} - </label>
      <button>Сохранить изменения</button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      coin: [],
      vendingMachineCoins: [],
    };
  },

  methods: {  
    async getCoins() {
      this.vendingMachineCoins = (
        await axios.get("https://192.168.0.77/api/VendingMachineCoins")
      ).data.filter(
        (x) => x.vendingMachinesId == this.$route.params.vendingname
      );

      this.coin = (
        await axios.get("https://192.168.0.77/api/Coins")
      ).data.filter((x) =>
        this.vendingMachineCoins.some((y) => y.coinsId == x.id)
      );
    },
    getAddCoin() {
      
    }
  },

  mounted() {
    this.getCoins();
  },
};
</script>

<style>
.container {
  margin-top: 30px;
  padding: 10px;
  justify-content: space-between;
  border: 3px solid gray;
}
</style>