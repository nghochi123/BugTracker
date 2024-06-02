import axios from "axios";
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import CssBaseline from "@mui/material/CssBaseline";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Topbar from "../../components/Topbar";
import TicketBar from "../../components/TicketBar";
import MainLayout from "../layout/MainLayout";
import { Ticket } from "../../types/ticket";
import { UserDisplay } from "../../types/user";
import { GETONEPROJECT } from "../../constants/env";
import CommentBox from "../../components/CommentBox";
import AddCommentBox from "../../components/AddCommentBox";
import { IComment } from "../../types/comment";

export default function ViewTicket() {
    const navigate = useNavigate();
    const { id, ticketId } = useParams<string>();
    const [ticket, setTicket] = useState<Ticket>();
    const [users, setUsers] = useState<UserDisplay[]>();
    const [comments, setComments] = useState<IComment[]>();
    const [open, setOpen] = useState<boolean>(false);
    const handleDialogOpen = () => {
        setOpen(true);
    };

    const handleDialogClose = (result: boolean) => {
        if (result) {
            axios
                .delete(`${GETONEPROJECT}/${id}/tickets/${ticketId}`, {
                    withCredentials: true,
                })
                .then(() => {
                    navigate(`/projects/${id}`);
                })
                .catch(() => {
                    navigate(`/projects/${id}`);
                });
        }
        setOpen(false);
    };
    const addCommentHandler = (comment: string) => {
        axios
            .post(
                `${GETONEPROJECT}/${id}/tickets/${ticketId}/comments`,
                {
                    content: comment,
                    ticketId,
                },
                { withCredentials: true }
            )
            .then(() => {
                navigate(0);
            })
            .catch(() => {
                navigate(0);
            });
    };
    useEffect(() => {
        axios
            .get(`${GETONEPROJECT}/${id}/tickets/${ticketId}`, {
                withCredentials: true,
            })
            .then((res) => {
                setTicket(res.data);
                setUsers(
                    res.data.assignedUserNames.map((userName: string) => {
                        return { userName };
                    })
                );
            })
            .catch(() => {
                navigate("/");
            });
        axios
            .get(`${GETONEPROJECT}/${id}/tickets/${ticketId}/comments`, {
                withCredentials: true,
            })
            .then((res) => {
                setComments(res.data);
            });
    }, [id]);
    return (
        <MainLayout>
            {ticket ? (
                <>
                    <CssBaseline />
                    <Container maxWidth="lg">
                        <Grid container spacing={1} sx={{ mt: 3 }}>
                            {users && id ? (
                                <>
                                    <Topbar
                                        title={ticket.title}
                                        description={ticket.description}
                                        users={users}
                                        deleteOpen={open}
                                        handleDeleteDialogOpen={
                                            handleDialogOpen
                                        }
                                        handleDeleteDialogClose={
                                            handleDialogClose
                                        }
                                        editUrl={`/projects/${id}/tickets/${ticketId}/edit`}
                                    />
                                    <TicketBar ticket={ticket} />
                                </>
                            ) : null}
                            {comments
                                ? comments.map((comment) => {
                                      return (
                                          <Grid item xs={12}>
                                              <CommentBox comment={comment} />
                                          </Grid>
                                      );
                                  })
                                : null}
                            <Grid item xs={12}>
                                <AddCommentBox
                                    addCommentHandler={addCommentHandler}
                                />
                            </Grid>
                        </Grid>
                    </Container>
                </>
            ) : null}
        </MainLayout>
    );
}
