import './assets/main.css'
import 'bootstrap/dist/css/bootstrap.min.css'

import { createApp } from 'vue'
import App from './App.vue'
import Vue3Filters from 'vue3-filters';

const app = createApp(App);

app.use(Vue3Filters);

app.config.globalProperties.$filters = Vue3Filters.filters

app.mount('#app');
