import { createRouter, createWebHistory } from 'vue-router'
import CoinsList from '@/views/CoinsList.vue'
import AdminMenu from '@/components/AdminMenu.vue'
import VendingMachine from '@/components/VendingMachine.vue'

const routes = [
  {
    path: '/',
    component: VendingMachine
  },
  {
    path: '/:vendingname',
    component: CoinsList
  },
  {
    path: '/admin/:vendingname',
    component: AdminMenu
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
