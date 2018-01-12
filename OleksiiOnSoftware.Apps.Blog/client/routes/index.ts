import {
  Home,
  HomePaged,
  HomeFilterByDate,
  HomeFilterByCategory,
  HomeFilterByTag,
  Post,
  NotFound
} from "components";

import { RouteProps } from "react-router";

interface AppRouteProps extends RouteProps {
  key: string;
}

const routes: AppRouteProps[] = [
  {
    key: "home",
    path: "/",
    exact: true,
    component: Home
  },
  {
    key: "page",
    path: "/page/:pageIndex",
    exact: true,
    component: HomePaged
  },
  {
    key: "homeFilterByDate",
    path: "/posts/date/:date",
    exact: true,
    component: HomeFilterByDate
  },
  {
    key: "homeFilterByCategory",
    path: "/posts/category/:categoryId",
    exact: true,
    component: HomeFilterByCategory
  },
  {
    key: "homeFilterByTag",
    path: "/posts/tag/:tagId",
    exact: true,
    component: HomeFilterByTag
  },
  {
    key: "post",
    path: "/post/:postId",
    exact: true,
    component: Post
  },
  {
    key: "not-found",
    component: NotFound
  }
];

export default routes;
