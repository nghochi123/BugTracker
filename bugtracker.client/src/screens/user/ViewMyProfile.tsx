import axios from "axios";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import Button from "@mui/material/Button";
import Box from "@mui/material/Box";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import MainLayout from "../layout/MainLayout";
import { GETUSER, UPDATEUSER } from "../../constants/env";

// interface UserData {
//     userName: string;
//     email: string;
// }

export default function ViewMyProfile() {
    const navigate = useNavigate();
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    const [error, setError] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);
    useEffect(() => {
        axios
            .get(`${GETUSER}/me`, { withCredentials: true })
            .then(({ data }) => {
                setUsername(data.userName);
                setEmail(data.email);
            })
            .catch(() => {
                navigate("/error");
            });
    }, []);
    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        axios
            .put(
                UPDATEUSER,
                {
                    username,
                    password,
                    email,
                },
                { withCredentials: true }
            )
            .then(() => {
                setError(false);
                setSuccess(true);
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
                    Your Profile
                </Typography>
                <Box
                    component="form"
                    onSubmit={handleSubmit}
                    noValidate
                    sx={{ mt: 1 }}
                >
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        id="username"
                        label="Username"
                        name="username"
                        autoComplete="username"
                        autoFocus
                        value={username}
                        InputProps={{
                            disabled: true,
                        }}
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        name="password"
                        label="Password"
                        type="password"
                        id="password"
                        autoComplete="current-password"
                        value={password}
                        onChange={(e) => {
                            setPassword(e.target.value);
                        }}
                    />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        name="email"
                        label="Email"
                        type="email"
                        id="email"
                        autoComplete="current-email"
                        value={email}
                        onChange={(e) => {
                            setEmail(e.target.value);
                        }}
                        error={error}
                        helperText={
                            error
                                ? "Username or password is incorrect"
                                : success
                                ? "User details updated successfully"
                                : null
                        }
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    >
                        Update
                    </Button>
                </Box>
            </Box>
        </MainLayout>
    );
}
