<template>
  <aside class="sidebar">
    <div class="sidebar__brand">Codex</div>

    <nav class="sidebar__nav">
      <RouterLink
        v-for="item in items"
        :key="item.path"
        :to="item.path"
        class="sidebar__link"
        active-class="sidebar__link--active"
      >
        {{ $t(item.labelKey) }}
      </RouterLink>
    </nav>
  </aside>
</template>

<script setup lang="ts">
interface NavItem {
  labelKey: string;
  path: string;
}

const items: NavItem[] = [
  { labelKey: 'nav.home', path: '/' },
  { labelKey: 'nav.projects', path: '/projects' },
  { labelKey: 'nav.asvs', path: '/asvs' },
];
</script>

<style scoped>
.sidebar {
  width: 240px;
  height: 100vh;
  padding: 1.5rem 1.25rem;
  box-sizing: border-box;

  display: flex;
  flex-direction: column;
  gap: 1.5rem;

  background: #050818; /* dark-ish base */
  border-right: 1px solid rgba(255, 255, 255, 0.06);
  color: #e5e7eb;
}

.sidebar__brand {
  font-size: 1.25rem;
  font-weight: 700;
  letter-spacing: 0.05em;
  text-transform: uppercase;
  text-align: center;
  color: #f9fafb;
}

.sidebar__nav {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.sidebar__link {
  display: block;
  padding: 0.6rem 0.75rem;
  border-radius: 0.5rem;
  font-size: 0.95rem;

  text-decoration: none;
  color: #9ca3af;

  transition:
    background-color 0.15s ease,
    color 0.15s ease,
    transform 0.08s ease;
}

.sidebar__link:hover {
  background: rgba(148, 163, 184, 0.12);
  color: #e5e7eb;
  transform: translateX(2px);
}

.sidebar__link--active {
  background: linear-gradient(57deg, #af54a1, #59c8e6);
  color: #f9fafb;
}
</style>
