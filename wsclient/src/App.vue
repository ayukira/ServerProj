<template>
<div>
  <v-home/>
</div>
</template>

<script>
import wsPage from './components/WS.vue'

export default {
  name: 'App',
  components: {
    "v-home":wsPage
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
