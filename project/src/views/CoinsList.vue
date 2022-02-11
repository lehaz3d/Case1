<template>
  <div>
    <label class="lbStyle">Внесенная сумма: {{ money }} руб. </label>
  </div>
  <div class="container border">
    <div class="container container__vertical">
      <label>1. Внесите деньги</label>
      <label>2. Выберите напиток</label>
      <label>3. Получите сдачу</label>
      <button class="buttonStyle" @click="sdachaPutCoin">Сдача</button>
      <button @click="$router.push(`/admin/${this.$route.params.vendingname}`)">
        Административное меню {{ this.$route.params.vendingname }} аппарата
      </button>
    </div>
    <div class="container__grid">
      <button
        v-for="coin in coinsList"
        @click="putCoin(coin)"
        :key="coin.id"
        class="buttonStyle"
        :disabled="lockedCoin(coin) == false"
      >
        {{ coin.name }}
      </button>
    </div>

    <div class="drinks container__grid container__grid__4">
      <button
        @click="sellingDrinks(drink)"
        class="drink-item container container__vertical"
        v-for="drink in drinks"
        :key="drink.id"
        :disabled="lockedDrinks(drink) == 0"
      >
        <img
          height="150"
          width="150"
          :src="getOutput(drink.image)"
          alt="none image"
        />
        <label style="font-size: 25px">{{ drink.name }}</label>
        <label style="font-size: 25px">{{ drink.cost }} руб.</label>
      </button>
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      drinks: [],
      money: 0,
      listVMDrinks: [],
      coinsList: [],
      vendingMachineCoins: [],
    };
  },

  methods: {
    lockedCoin(coin) {
      return this.vendingMachineCoins.some(
        (x) => x.coinsId == coin.id && x.isActive == true
      );
    },

    lockedDrinks(drink) {
      return this.listVMDrinks.some(
        (x) => x.drinksId == drink.id && x.count > 0
      );
    },

    async sdachaPutCoin() {
      const minusSdachaCoin = this.vendingMachineCoins.filter(
        (x) => x.count > 0
      );
      const coins = this.coinsList.filter((x) =>
        minusSdachaCoin.some((y) => y.coinsId == x.id)
      );
      coins.reverse();

      for (let i = 0; i < coins.length; i++) {
        const parseCoin = parseInt(coins[i].name);

        if (this.money <= 0 && parseCoin > this.money) {
          continue;
        }

        const findCoin = minusSdachaCoin.find((x) => x.coinsId == coins[i].id);
        const resultMin = Math.floor(
          Math.min(findCoin.count, this.money / parseCoin)
        );


        if (findCoin.count >= resultMin && this.money % parseCoin > 0) {
          resultMin -= 1;
        }

        this.money -= resultMin * parseCoin;
        const resultCoins = this.vendingMachineCoins.find(
          (x) => x.coinsId == coins[i].id
        );

        resultCoins.count -= resultMin;
        await axios.put(
          `https://192.168.0.77/api/VendingMachineCoins/${resultCoins.id}`,
          resultCoins
        );
      }
      alert("Успешный вывод сдачи")
    },

    async putCoin(coin) {
      this.money += parseInt(coin.name);
      const addCoin = this.vendingMachineCoins.find(
        (x) => x.coinsId == coin.id
      );
      addCoin.count += 1;

      await axios.put(
        `https://192.168.0.77/api/VendingMachineCoins/${addCoin.id}`,
        addCoin
      );
    },

    getOutput(image) {
      return (
        "data:image;base64," + (image === null ? "" : image.toString("base64"))
      );
    },

    async sellingDrinks(drink) {
      try {
        if (this.money < drink.cost) {
          alert("Внесенная цена не соответстует цене напитка");
          return this.money;
        }
        this.money -= drink.cost;

        const minusCount = this.listVMDrinks.find(
          (x) => x.drinksId == drink.id
        );
        minusCount.count -= 1;
        await axios.put(
          `https://192.168.0.77/api/VendingMachineDrinks/${minusCount.id}`,
          minusCount
        );

        alert("Сдача равна: " + this.money);
        return this.money;
      } catch (ex) {
        alert(ex);
      }
    },

    async getData() {
      try {
        const listVendingMachineDrinks = await axios.get(
          "https://192.168.0.77/api/VendingMachineDrinks"
        );
        this.listVMDrinks = listVendingMachineDrinks.data.filter(
          (x) => x.vendingMachinesId == this.$route.params.vendingname
        );

        const listDrinks = await axios.get("https://192.168.0.77/api/Drinks");
        this.drinks = listDrinks.data.filter((x) =>
          this.listVMDrinks.some((y) => y.drinksId == x.id)
        );
        this.getCoins();
      } catch (ex) {
        alert(ex);
      }
    },

    async getCoins() {
      this.vendingMachineCoins = (
        await axios.get("https://192.168.0.77/api/VendingMachineCoins")
      ).data.filter(
        (x) => x.vendingMachinesId == this.$route.params.vendingname
      );

      this.coinsList = (
        await axios.get("https://192.168.0.77/api/Coins")
      ).data.filter((x) =>
        this.vendingMachineCoins.some((y) => y.coinsId == x.id)
      );
    },
  },

  mounted() {
    this.getData();
  },
};
</script>

<style lang="scss" scoped>
.buttonStyle {
  height: 150px;
  width: 150px;
  margin: 50px;
}

.lbStyle {
  font-size: 35px;
  border: 3px solid gray;
  padding: 5px;
}

.drinks {
  background-color: lightgrey;
}

.drink-item {
  margin: 5px;
}

.container {
  display: flex;
  margin-top: 20px;
  justify-content: space-between;

  &__vertical {
    flex-direction: column;
    padding: 20px;
    align-items: center;
  }

  &.border {
    border: 3px solid gray;
  }

  &__grid {
    background: lightgrey;
    text-align: center;
    display: grid;
    grid-template-columns: auto auto;
    justify-content: space-around;
    align-items: flex-start;

    &__4 {
      grid-template-columns: auto auto auto auto;
      padding: 20px;
    }

    &.border {
      border: 3px solid gray;
    }
  }
}
</style>
