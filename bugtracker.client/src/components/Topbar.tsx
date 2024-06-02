import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import Link from "@mui/material/Link";
import Stack from "@mui/material/Stack";
import { UserDisplay } from "../types/user";
import { Button } from "@mui/material";
import ConfirmationDialog from "./ConfirmationDialog";
import { useNavigate } from "react-router-dom";

interface TopbarProps {
    users: UserDisplay[];
    description: string;
    title: string;
    editUrl: string;
    deleteOpen: boolean;
    handleDeleteDialogOpen: () => void;
    handleDeleteDialogClose: (result: boolean) => void;
}

export default function Topbar(props: TopbarProps) {
    const {
        users,
        description,
        title,
        editUrl,
        deleteOpen,
        handleDeleteDialogOpen,
        handleDeleteDialogClose,
    } = props;
    const navigate = useNavigate();
    return (
        <Grid item xs={12}>
            <ConfirmationDialog
                open={deleteOpen}
                handleClose={handleDeleteDialogClose}
            />
            <Paper elevation={0} sx={{ p: 2, bgcolor: "grey.200" }}>
                <div
                    style={{
                        display: "flex",
                        marginBottom: "10px",
                    }}
                >
                    <Stack spacing={2} direction="row">
                        <Button
                            onClick={() => {
                                navigate(editUrl);
                            }}
                            variant="outlined"
                        >
                            Edit
                        </Button>
                        <Button
                            onClick={handleDeleteDialogOpen}
                            variant="outlined"
                            color="error"
                        >
                            Delete
                        </Button>
                    </Stack>
                </div>
                <Grid container>
                    <Grid item xs={8}>
                        <Typography variant="h6" gutterBottom>
                            {title}
                        </Typography>
                        <Typography>{description}</Typography>
                    </Grid>
                    <Grid item xs={4}>
                        <Typography variant="h6" gutterBottom>
                            Users
                        </Typography>
                        {users.map((item) => (
                            <Link
                                display="block"
                                variant="body1"
                                href={`/users/${item.userName}`}
                                key={item.userName}
                            >
                                {item.userName}
                            </Link>
                        ))}
                    </Grid>
                </Grid>
            </Paper>
        </Grid>
    );
}
