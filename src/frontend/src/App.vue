<template>
  <div class="app-shell">
    <TheSidebar />
    <!-- <header>
      <h1>ByteGuard Codex</h1>
      <p>Codify and track your application security controls.</p>
    </header> -->
    <main class="app-shell__content">
      <RouterView />
    </main>
  </div>
</template>

<script setup lang="ts">
import TheSidebar from '@/components/TheSidebar.vue';
import { RouterLink, RouterView } from 'vue-router';
</script>

<style scoped>
/* Layout: sidebar on the left, content on the right */
.app-shell {
  display: flex;
  min-height: 100vh;
}

/* Content should take remaining width */
.app-shell__content {
  flex: 1;
  padding: 1.5rem 2rem;
  box-sizing: border-box;
}
</style>
