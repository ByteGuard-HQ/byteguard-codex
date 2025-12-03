import { createRouter, createWebHistory } from 'vue-router';
import { i18n } from '@/i18n';

import HomeView from '@/views/HomeView.vue';
import ProjectsView from '@/views/ProjectsView.vue';
import AsvsView from '@/views/AsvsView.vue';

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
    },
    {
      path: '/projects',
      name: 'Projects',
      component: ProjectsView,
      meta: {
        titleKey: 'nav.projects',
      },
    },
    {
      path: '/asvs',
      name: 'ASVS',
      component: AsvsView,
      meta: {
        titleKey: 'nav.asvs',
      },
    },
  ],
});

const BASE_TITLE = 'ByteGuard Codex';

router.afterEach((to) => {
  const { t } = i18n.global;

  const pageTitleKey = to.meta?.titleKey as string | undefined;

  if (pageTitleKey) {
    document.title = `${BASE_TITLE} - ${t(pageTitleKey)}`;
  } else {
    document.title = BASE_TITLE;
  }
});

export default router;
