import axios from "axios";
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import CssBaseline from "@mui/material/CssBaseline";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Topbar from "../../components/Topbar";
import Tickets from "../../components/Tickets";
import MainLayout from "../layout/MainLayout";
import { Ticket } from "../../types/ticket";
import { TableDisplay } from "../../types/tabledisplay";
import { User, UserDisplay } from "../../types/user";
import { Project } from "../../types/project";
import { GETONEPROJECT } from "../../constants/env";

export default function ViewProject() {
    const navigate = useNavigate();
    const { id } = useParams<string>();
    const [project, setProject] = useState<Project>();
    const [openTickets, setOpenTickets] = useState<TableDisplay[]>([]);
    const [ipTickets, setIPTickets] = useState<TableDisplay[]>([]);
    const [closedTickets, setClosedTickets] = useState<TableDisplay[]>([]);
    const [users, setUsers] = useState<UserDisplay[]>();
    const [openDelete, setOpenDelete] = useState<boolean>(false);
    const handleDeleteDialogOpen = () => {
        setOpenDelete(true);
    };

    const handleDeleteDialogClose = (result: boolean) => {
        if (result) {
            axios
                .delete(`${GETONEPROJECT}/${id}`, { withCredentials: true })
                .then(() => {
                    navigate("/");
                })
                .catch(() => {
                    navigate("/");
                });
        }
        setOpenDelete(false);
    };
    const handleClick = (
        event: React.MouseEvent<unknown>,
        ticketId: number
    ) => {
        navigate(`/projects/${id}/tickets/${ticketId}`);
    };
    useEffect(() => {
        axios
            .get(`${GETONEPROJECT}/${id}`, { withCredentials: true })
            .then((res) => {
                setProject(res.data);
            })
            .catch(() => {
                navigate("/");
            });
        axios
            .get(`${GETONEPROJECT}/${id}/users`, { withCredentials: true })
            .then((res) => {
                setUsers(
                    res.data.map((item: User) => {
                        return { userName: item.userName };
                    })
                );
            })
            .catch(() => {
                navigate("/");
            });
        axios
            .get(`${GETONEPROJECT}/${id}/tickets`, { withCredentials: true })
            .then((res) => {
                setOpenTickets(
                    res.data
                        .filter((item: Ticket) => item.status === 1)
                        .map((item: Ticket) => {
                            return {
                                id: item.id,
                                name: item.title,
                                description: item.description,
                                createdAt: new Date(item.createdAt),
                                updatedAt: new Date(item.updatedAt),
                            };
                        })
                );
                setIPTickets(
                    res.data
                        .filter((item: Ticket) => item.status === 2)
                        .map((item: Ticket) => {
                            return {
                                id: item.id,
                                name: item.title,
                                description: item.description,
                                createdAt: new Date(item.createdAt),
                                updatedAt: new Date(item.updatedAt),
                            };
                        })
                );
                setClosedTickets(
                    res.data
                        .filter((item: Ticket) => item.status === 0)
                        .map((item: Ticket) => {
                            return {
                                id: item.id,
                                name: item.title,
                                description: item.description,
                                createdAt: new Date(item.createdAt),
                                updatedAt: new Date(item.updatedAt),
                            };
                        })
                );
            })
            .catch(() => {
                navigate("/");
            });
    }, [id]);
    return (
        <MainLayout>
            {openTickets || ipTickets || closedTickets ? (
                <>
                    <CssBaseline />
                    <Container maxWidth="lg">
                        <Grid container spacing={5} sx={{ mt: 3 }}>
                            {project && users && id ? (
                                <>
                                    <Topbar
                                        title={project.name}
                                        description={project.description}
                                        users={users}
                                        deleteOpen={openDelete}
                                        handleDeleteDialogOpen={
                                            handleDeleteDialogOpen
                                        }
                                        handleDeleteDialogClose={
                                            handleDeleteDialogClose
                                        }
                                        editUrl={`/projects/${id}/edit`}
                                    />
                                    <Tickets
                                        title="Open"
                                        createNew={"createticket"}
                                        rows={openTickets || []}
                                        navigate={navigate}
                                        handleClick={handleClick}
                                    />
                                    <Tickets
                                        title="In Progress"
                                        rows={ipTickets || []}
                                        navigate={navigate}
                                        handleClick={handleClick}
                                    />
                                    <Tickets
                                        title="Closed"
                                        rows={closedTickets || []}
                                        navigate={navigate}
                                        handleClick={handleClick}
                                    />
                                </>
                            ) : null}
                        </Grid>
                    </Container>
                </>
            ) : null}
        </MainLayout>
    );
}
