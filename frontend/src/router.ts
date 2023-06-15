import { createRouter, createWebHistory } from "vue-router";
import HomeView from "./views/HomeView.vue";
import { useAccountStore } from "@/stores/account";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      name: "Home",
      path: "/",
      component: HomeView,
      meta: { isPublic: true },
    },
    {
      name: "About",
      path: "/about",
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import("./views/AboutView.vue"),
      meta: { isPublic: true },
    },
    // Users
    {
      name: "Confirm",
      path: "/confirm",
      component: () => import("./views/Users/Confirm.vue"),
      meta: { isPublic: true },
    },
    {
      name: "Profile",
      path: "/profile",
      component: () => import("./views/Users/Profile.vue"),
    },
    {
      name: "RecoverPassword",
      path: "/recover-password",
      component: () => import("./views/Users/RecoverPassword.vue"),
      meta: { isPublic: true },
    },
    {
      name: "Register",
      path: "/register",
      component: () => import("./views/Users/Register.vue"),
      meta: { isPublic: true },
    },
    {
      name: "ResetPassword",
      path: "/reset-password",
      component: () => import("./views/Users/ResetPassword.vue"),
      meta: { isPublic: true },
    },
    {
      name: "SignIn",
      path: "/sign-in",
      component: () => import("./views/Users/SignIn.vue"),
      meta: { isPublic: true },
    },
    {
      name: "SignOut",
      path: "/sign-out",
      component: () => import("./views/Users/SignOut.vue"),
    },
    // Realms
    {
      name: "RealmList",
      path: "/realms",
      component: () => import("./views/Realms/RealmList.vue"),
    },
    {
      name: "RealmEdit",
      path: "/realms/:id",
      component: () => import("./views/Realms/RealmEdit.vue"),
    },
    {
      name: "CreateRealm",
      path: "/create-realm",
      component: () => import("./views/Realms/RealmEdit.vue"),
    },
    // NotFound
    {
      name: "NotFound",
      path: "/:pathMatch(.*)*",
      component: () => import("./views/NotFound.vue"),
      meta: { isPublic: true },
    },
  ],
});

router.beforeEach((to) => {
  const accountStore = useAccountStore();
  if (!to.meta.isPublic && !accountStore.authenticated) {
    return { name: "SignIn", query: { redirect: to.fullPath } };
  }
});

export default router;
