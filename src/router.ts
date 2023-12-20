import { createRouter, createWebHistory } from "vue-router";

import HomeView from "@/views/HomeView.vue";
import { useAccountStore } from "./stores/account";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: HomeView,
      meta: { isPublic: true },
    },
    // Account
    {
      path: "/confirm/:token",
      name: "Confirm",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("@/views/account/ConfirmView.vue"),
      meta: { isPublic: true },
    },
    {
      path: "/profile",
      name: "Profile",
      component: () => import("@/views/account/ProfileView.vue"),
    },
    {
      path: "/recover-password",
      name: "RecoverPassword",
      component: () => import("@/views/account/RecoverPasswordView.vue"),
      meta: { isPublic: true },
    },
    {
      path: "/register",
      name: "Register",
      component: () => import("@/views/account/RegisterView.vue"),
      meta: { isPublic: true },
    },
    {
      path: "/reset-password/:token",
      name: "ResetPassword",
      component: () => import("@/views/account/ResetPasswordView.vue"),
      meta: { isPublic: true },
    },
    {
      path: "/sign-in",
      name: "SignIn",
      component: () => import("@/views/account/SignInView.vue"),
      meta: { isPublic: true },
    },
    {
      path: "/sign-out",
      name: "SignOut",
      component: () => import("@/views/account/SignOutView.vue"),
    },
  ],
});

router.beforeEach((to) => {
  const account = useAccountStore();
  if (!to.meta.isPublic && !account.authenticated) {
    return { name: "SignIn", query: { redirect: to.fullPath } };
  }
});

export default router;
