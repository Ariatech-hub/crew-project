const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    meta: { requiresAuth: true },
    children: [
      { path: "", component: () => import("pages/Index.vue") },
      {
        path: "/setting",
        component: () => import("pages/Setting/Index.vue"),
        meta: { requiresAuth: true, menuId: 1 },
        children: [
          {
            path: "menu-management",
            component: () => import("pages/Setting/MenuManagement.vue"),
            meta: { menuId: 2, requiresAuth: true },
          },
          {
            path: "user-management",
            component: () => import("pages/Setting/UserManagement.vue"),
            meta: { menuId: 3, requiresAuth: true },
          },
          {
            path: "education-level",
            component: () => import("pages/Setting/EducationLevel.vue"),
            meta: { menuId: 4, requiresAuth: true },
          },
          {
            path: "districts",
            component: () => import("pages/Setting/District.vue"),
            meta: { menuId: 5, requiresAuth: true },
          },
          {
            path: "palikas",
            component: () => import("pages/Setting/Palika.vue"),
            meta: { menuId: 6, requiresAuth: true },
          },
          {
            path: "communities",
            component: () => import("pages/Setting/Community.vue"),
            meta: { menuId: 7, requiresAuth: true },
          },
          {
            path: "ethnicities",
            component: () => import("pages/Setting/Ethnicity.vue"),
            meta: { menuId: 7, requiresAuth: true },
          },
          {
            path: ":id/menu-access-control",
            component: () => import("pages/Setting/MenuAccessControl.vue"),
            meta: { menuId: 9, requiresAuth: true },
          },
          {
            path: "grains",
            component: () => import("pages/Setting/Grain.vue"),
            meta: { menuId: 10, requiresAuth: true },
          },
          {
            path: "internet-use",
            component: () => import("pages/Setting/InternetUse.vue"),
            meta: { menuId: 11, requiresAuth: true },
          },
          {
            path: "internet-type",
            component: () => import("pages/Setting/InternetType.vue"),
            meta: { menuId: 12, requiresAuth: true },
          },
          {
            path: "obstacles",
            component: () => import("pages/Setting/Obstacle.vue"),
            meta: { menuId: 13, requiresAuth: true },
          },
          {
            path: "occupations",
            component: () => import("pages/Setting/Occupation.vue"),
            meta: { menuId: 17, requiresAuth: true },
          },
          {
            path: "income-sources",
            component: () => import("pages/Setting/IncomeSources.vue"),
            meta: { menuId: 18, requiresAuth: true },
          },
          {
            path: "labour-division",
            component: () => import("pages/Setting/LabourDivision.vue"),
            meta: { menuId: 19, requiresAuth: true },
          },
          {
            path: "provinces",
            component: () => import("pages/Setting/Province.vue"),
            meta: { menuId: 20, requiresAuth: true },
          },
          {
            path: "participation-option",
            component: () => import("pages/Setting/ParticipationOption.vue"),
            meta: { menuId: 21, requiresAuth: true },
          },
          {
            path: "year",
            component: () => import("pages/Setting/Year.vue"),
            meta: { menuId: 21, requiresAuth: true },
          },
          {
            path: "location",
            component: () => import("pages/Setting/Location.vue"),
            meta: { menuId: 25, requiresAuth: true },
          },
          {
            path: "marital-status",
            component: () => import("pages/Setting/MaritalStatus.vue"),
            meta: { menuId: 25, requiresAuth: true },
          },
          {
            path: "card-image",
            component: () => import("src/pages/Setting/CardImage.vue"),
            meta: { menuId: 26, requiresAuth: true },
          },
        ],
      },
      {
        path: "/farmer",
        component: () => import("pages/Farmer/Index.vue"),
        meta: { requiresAuth: true, menuId: 14 },
        children: [
          {
            path: "list",
            component: () => import("pages/Farmer/List.vue"),
            meta: { menuId: 14, requiresAuth: true },
          },
          {
            path: ":id/view",
            component: () => import("pages/Farmer/View.vue"),
            meta: { menuId: 14, requiresAuth: true },
          },
          {
            path: "generate-card",
            component: () => import("pages/Farmer/GenerateCard.vue"),
            meta: { requiresAuth: true, menuId: 27 },
          },
          {
            path: "production-plan",
            component: () => import("pages/Farmer/ProductionPlan.vue"),
            meta: { requiresAuth: true, menuId: 27 },
          },
        ],
      },
      {
        path: "/dispatch",
        component: () => import("pages/Dispatch/Dispatch.vue"),
        meta: { requiresAuth: true, menuId: 34 },
      },
      {
        path: "/account/list",
        component: () => import("pages/Account/list.vue"),
        meta: { requiresAuth: true, menuId: 36 },
      },

      {
        path: "/grain-cycle",
        component: () => import("pages/Farmer/Index.vue"),
        meta: { requiresAuth: true, menuId: 14 },
        children: [
          {
            path: "list",
            component: () => import("pages/GrainCycle/List.vue"),
            meta: { requiresAuth: true, menuId: 24 },
          },
          {
            path: "new",
            component: () => import("pages/GrainCycle/New.vue"),
            meta: { menuId: 14, requiresAuth: true },
          },
          {
            path: ":id/update",
            component: () => import("pages/GrainCycle/Update.vue"),
            meta: { requiresAuth: true },
          },
        ],
      },
      {
        path: "/report",
        component: () => import("pages/Report/Index.vue"),
        meta: { requiresAuth: true, menuId: 15 },
        children: [
          {
            path: "farmer",
            component: () => import("pages/Report/Farmer.vue"),
            meta: { menuId: 16, requiresAuth: true },
          },
        ],
      },
      {
        path: "/customer",
        component: () => import("pages/Customer/Index.vue"),
        meta: { requiresAuth: true },
        children: [
          {
            path: "list",
            component: () => import("pages/Customer/List.vue"),
            meta: { requiresAuth: true, menuId: 29 },
          },
          {
            path: "new",
            component: () => import("pages/Customer/New.vue"),
            meta: { requiresAuth: true },
          },
          {
            path: ":id/view",
            component: () => import("pages/Customer/View.vue"),
            meta: { requiresAuth: true },
          },
          {
            path: "dispatched",
            component: () => import("pages/Customer/Dispatched.vue"),
            meta: { requiresAuth: true },
          },
          {
            path: ":customerId/buyer-grain/:id",
            component: () => import("pages/Customer/BuyerGrainView.vue"),
            meta: { requiresAuth: true },
          },
        ],
      },
      {
        path: "/stock",
        component: () => import("pages/Stock/Index.vue"),
        meta: { requiresAuth: true },
        children: [
          {
            path: "list",
            component: () => import("pages/Stock/List.vue"),
            meta: { requiresAuth: true, menuId: 30 },
          },
        ],
      },
      {
        path: "/dashboard",
        component: () => import("pages/dashboard/Dashboard.vue"),
        meta: { requiresAuth: true },
      },
      {
        path: "/purchase",
        component: () => import("pages/Purchase/Index.vue"),
        meta: { requiresAuth: true },
        children: [
          {
            path: "list",
            component: () => import("pages/Purchase/List.vue"),
            meta: { requiresAuth: true, menuId: 37 },
          },
        ],
      },
    ],
  },
  {
    path: "/login",
    component: () => import("pages/Login.vue"),
  },

  // Always leave this as last one,
  // but you can also remove it
  {
    path: "/:catchAll(.*)*",
    component: () => import("pages/Error404.vue"),
  },
];

export default routes;
