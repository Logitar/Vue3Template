import { createRouter, createWebHistory } from "vue-router";
import HomeView from "@/views/HomeView.vue";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: "/",
      name: "Home",
      component: HomeView,
    },
    // Account
    {
      path: "/confirm/:token",
      name: "Confirm",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("@/views/account/ConfirmView.vue"),
    },
    {
      path: "/profile",
      name: "Profile",
      component: () => import("@/views/account/ProfileView.vue"),
    },
    {
      path: "/register",
      name: "Register",
      component: () => import("@/views/account/RegisterView.vue"),
    },
    {
      path: "/sign-in",
      name: "SignIn",
      component: () => import("@/views/account/SignInView.vue"),
    },
    {
      path: "/sign-out",
      name: "SignOut",
      component: () => import("@/views/account/SignOutView.vue"),
    },
  ],
});

export default router;
