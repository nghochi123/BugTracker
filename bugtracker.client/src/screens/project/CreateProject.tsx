import axios from "axios";
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Grid from "@mui/material/Grid";
import {
    Box,
    Button,
    CssBaseline,
    TextField,
    Select,
    MenuItem,
    OutlinedInput,
    Chip,
    InputLabel,
    FormControl,
    FormHelperText,
} from "@mui/material";
import Typography from "@mui/material/Typography";
import MainLayout from "../layout/MainLayout";
import { CREATEPROJECT, GETUSER, GETONEPROJECT } from "../../constants/env";
import { User } from "../../types/user";

export default function CreateProject() {
    const navigate = useNavigate();
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [error, setError] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);
    const [assignedUsers, setAssignedUsers] = useState<string[]>([]);
    const [availableUsers, setAvailableUsers] = useState<string[]>([]);
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        axios
            .post(
                CREATEPROJECT,
                {
                    name: title,
                    description,
                },
                { withCredentials: true }
            )
            .then((res) => {
                setError(false);
                setSuccess(true);
                for (const user of assignedUsers) {
                    axios.post(
                        `${GETONEPROJECT}/${res.data}/users`,
                        {
                            username: user,
                            projectId: res.data.toString(),
                        },
                        { withCredentials: true }
                    );
                }
                navigate("/");
            })
            .catch(() => {
                setSuccess(false);
                setError(true);
            });
    };
    useEffect(() => {
        axios.get(GETUSER, { withCredentials: true }).then((res) => {
            setAvailableUsers(
                res.data.map((user: User) => {
                    return user.userName;
                })
            );
        });
    }, []);
    return (
        <MainLayout>
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 8,
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                }}
            >
                <Typography component="h1" variant="h5">
                    Create new project
                </Typography>
                <div style={{ margin: "3%" }} />
                <Grid container spacing={3}>
                    <Box
                        component="form"
                        onSubmit={handleSubmit}
                        noValidate
                        sx={{ mt: 1, width: 500, maxWidth: "100%" }}
                    >
                        <Grid container spacing={3}>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    id="title"
                                    label="Title"
                                    name="title"
                                    autoFocus
                                    value={title}
                                    onChange={(e) => {
                                        setTitle(e.target.value);
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    name="description"
                                    label="Description"
                                    type="description"
                                    id="description"
                                    value={description}
                                    onChange={(e) => {
                                        setDescription(e.target.value);
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth>
                                    <InputLabel id="assign-users-label">
                                        Users
                                    </InputLabel>
                                    <Select
                                        fullWidth
                                        required
                                        labelId="assign-users-label"
                                        id="assign-users"
                                        multiple
                                        value={assignedUsers}
                                        onChange={(e) => {
                                            setAssignedUsers(
                                                typeof e.target.value ===
                                                    "string"
                                                    ? e.target.value.split(",")
                                                    : e.target.value
                                            );
                                        }}
                                        input={<OutlinedInput label="Chip" />}
                                        renderValue={(selected) => (
                                            <Box
                                                sx={{
                                                    display: "flex",
                                                    flexWrap: "wrap",
                                                    gap: 0.5,
                                                }}
                                            >
                                                {selected.map((value) => (
                                                    <Chip
                                                        key={value}
                                                        label={value}
                                                    />
                                                ))}
                                            </Box>
                                        )}
                                        MenuProps={{}}
                                    >
                                        {availableUsers.map((name) => (
                                            <MenuItem key={name} value={name}>
                                                {name}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12}>
                                <FormHelperText>
                                    {error
                                        ? "Failed to create a project"
                                        : success
                                        ? "Project created successfully. Redirecting..."
                                        : null}
                                </FormHelperText>
                            </Grid>
                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Create
                        </Button>
                    </Box>
                </Grid>
            </Box>
        </MainLayout>
    );
}
