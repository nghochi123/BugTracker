import axios from "axios";
import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import {
    Box,
    Button,
    CssBaseline,
    TextField,
    Select,
    MenuItem,
    OutlinedInput,
    Chip,
    FormHelperText,
    InputLabel,
    FormControl,
} from "@mui/material";
import MainLayout from "../layout/MainLayout";
import { GETONEPROJECT } from "../../constants/env";
import { User } from "../../types/user";

const availableTags = [
    "Bug",
    "Improvement",
    "New Feature",
    "Testing",
    "Development",
];

export default function CreateTicket() {
    const navigate = useNavigate();
    const { id } = useParams<string>();
    const [title, setTitle] = useState<string>("");
    const [description, setDescription] = useState<string>("");
    const [priority, setPriority] = useState<string>("");
    const [status, setStatus] = useState<string>("");
    const [tags, setTags] = useState<string[]>([]);
    const [assignedUsers, setAssignedUsers] = useState<string[]>([]);
    const [availableUsers, setAvailableUsers] = useState<string[]>([]);
    const [error, setError] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);
    useEffect(() => {
        axios
            .get(`${GETONEPROJECT}/${id}/users`, { withCredentials: true })
            .then((res) => {
                setAvailableUsers(
                    res.data.map((item: User) => {
                        return item.userName;
                    })
                );
            })
            .catch(() => {
                navigate("/");
            });
    }, []);
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        axios
            .post(
                `${GETONEPROJECT}/${id}/tickets`,
                {
                    title,
                    description,
                    priority,
                    status,
                    tags,
                    assignedUserNames: assignedUsers,
                },
                { withCredentials: true }
            )
            .then(() => {
                setError(false);
                setSuccess(true);
                navigate(`/projects/${id}`);
            })
            .catch(() => {
                setSuccess(false);
                setError(true);
            });
    };
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
                    Create new ticket
                </Typography>
                <div style={{ margin: "3%" }} />
                <Grid container spacing={3}>
                    <Box
                        component="form"
                        onSubmit={handleSubmit}
                        noValidate
                        sx={{
                            mt: 1,
                            width: 500,
                            maxWidth: "100%",
                        }}
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
                                    id="description"
                                    label="Description"
                                    name="description"
                                    value={description}
                                    onChange={(e) => {
                                        setDescription(e.target.value);
                                    }}
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth>
                                    <InputLabel id="priority">
                                        Priority
                                    </InputLabel>
                                    <Select
                                        fullWidth
                                        required
                                        labelId="priority"
                                        id="priority"
                                        value={priority}
                                        label="Priority"
                                        placeholder="Priority"
                                        onChange={(e) => {
                                            setPriority(e.target.value);
                                        }}
                                    >
                                        {[1, 2, 3, 4, 5, 6, 7, 8, 9, 10].map(
                                            (value) => {
                                                return (
                                                    <MenuItem value={value}>
                                                        {value == 1
                                                            ? `1 (Highest Priority)`
                                                            : value === 10
                                                            ? `10 (Lowest Priority)`
                                                            : value}
                                                    </MenuItem>
                                                );
                                            }
                                        )}
                                    </Select>
                                </FormControl>
                            </Grid>
                            <Grid item xs={12}>
                                <FormControl fullWidth>
                                    <InputLabel id="status">Status</InputLabel>
                                    <Select
                                        fullWidth
                                        required
                                        labelId="status"
                                        id="status"
                                        value={status}
                                        label="Status"
                                        placeholder="status"
                                        onChange={(e) => {
                                            setStatus(e.target.value);
                                        }}
                                    >
                                        {[
                                            { text: "Closed", value: 0 },
                                            { text: "Open", value: 1 },
                                            { text: "In Progress", value: 2 },
                                        ].map((item) => {
                                            return (
                                                <MenuItem value={item.value}>
                                                    {item.text}
                                                </MenuItem>
                                            );
                                        })}
                                    </Select>
                                </FormControl>
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
                                <FormControl fullWidth>
                                    <InputLabel id="set-tags-label">
                                        Tags
                                    </InputLabel>
                                    <Select
                                        fullWidth
                                        required
                                        labelId="set-tags-label"
                                        id="set-tags"
                                        multiple
                                        value={tags}
                                        onChange={(e) => {
                                            setTags(
                                                typeof e.target.value ===
                                                    "string"
                                                    ? e.target.value.split(",")
                                                    : e.target.value
                                            );
                                        }}
                                        input={
                                            <OutlinedInput
                                                id="select-multiple-chip"
                                                label="Chip"
                                            />
                                        }
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
                                        error={error}
                                        MenuProps={{}}
                                    >
                                        {availableTags.map((name) => (
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
                                        ? "Failed to create a ticket"
                                        : success
                                        ? "Ticket created successfully. Redirecting..."
                                        : null}
                                </FormHelperText>
                            </Grid>
                            <Grid item xs={12}>
                                <Button
                                    type="submit"
                                    fullWidth
                                    variant="contained"
                                    sx={{ mb: 2 }}
                                >
                                    Create
                                </Button>
                            </Grid>
                        </Grid>
                    </Box>
                </Grid>
            </Box>
        </MainLayout>
    );
}
