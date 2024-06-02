import { RouteObject } from "react-router-dom";
import SignIn from "../screens/user/SignIn";
import SignUp from "../screens/user/SignUp";
import ViewMyProfile from "../screens/user/ViewMyProfile";
import ViewProfile from "../screens/user/ViewProfile";
import CreateProject from "../screens/project/CreateProject";
import ViewProject from "../screens/project/ViewProject";
import EditProject from "../screens/project/EditProject";
import CreateTicket from "../screens/ticket/CreateTicket";
import ViewTicket from "../screens/ticket/ViewTicket";
import EditTicket from "../screens/ticket/EditTicket";
import Home from "../screens/dashboard/Home";
import Error from "../screens/misc/Error";

export const ROUTES: RouteObject[] = [
    {
        path: "/",
        element: <Home />,
    },
    {
        path: "/login",
        element: <SignIn />,
    },
    {
        path: "/register",
        element: <SignUp />,
    },
    {
        path: "/users/me",
        element: <ViewMyProfile />,
    },
    {
        path: "/users/:id",
        element: <ViewProfile />,
    },
    {
        path: "/createproject",
        element: <CreateProject />,
    },
    {
        path: "/projects/:id",
        element: <ViewProject />,
    },
    {
        path: "/projects/:id/edit",
        element: <EditProject />,
    },
    {
        path: "/projects/:id/createticket",
        element: <CreateTicket />,
    },
    {
        path: "/projects/:id/tickets/:ticketId",
        element: <ViewTicket />,
    },
    {
        path: "/projects/:id/tickets/:ticketId/edit",
        element: <EditTicket />,
    },
    {
        path: "/error",
        element: <Error />,
    },
];
