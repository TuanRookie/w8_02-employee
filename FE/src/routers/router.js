import { createRouter,createWebHistory } from "vue-router";

import Customers from '../views/customer/Customers'
import Report from '../views/Report'
import Statistics from '../views/Statistics'
import EmployeeList from '../views/employee/EmployeeList'
import TheHome from '../components/TheHome'
import TheLogin from '../components/TheLogin'

const routes = [
    {
        path: '/',
        component: TheHome, 
        meta: { requiresAuth: true },
        children: [
            {
                path: 'employee',
                component: EmployeeList
            },
            {
                path: 'report',
                component: Report
            },
            {
                path: 'statistics',
                component: Statistics
            },
            {
                path: 'customer',
                component: Customers
            },
            
            // Các route con cho TheHome nếu cần
        ]
    },
    {
        path: '/login',
        component: TheLogin, // Thêm route cho ThePage
    },
    

];

const router = createRouter({
    history: createWebHistory(),
    routes
});

router.beforeEach((to, from, next) => {
  const token = JSON.parse(localStorage.getItem("Token"));
  if (
    // to.matched.some((route) => route.meta.requiresAuth) &&
    to.meta.requiresAuth &&
    !token
  ) {
    next("/login"); // Chuyển hướng đến trang login nếu cố gắng truy cập vào trang user
  } else if (to.path === "/login" && token) {
    next("/");
  } else {
    next();
  }
});

export default router;