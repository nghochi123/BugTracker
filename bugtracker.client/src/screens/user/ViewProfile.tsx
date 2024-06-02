import axios from "axios";
import { useState, useEffect } from "react";
import Box from "@mui/material/Box";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import { useParams } from "react-router-dom";
import MainLayout from "../layout/MainLayout";
import { GETUSER } from "../../constants/env";

// interface UserData {
//     userName: string;
//     email: string;
// }

export default function ViewProfile() {
    const { id } = useParams<string>();
    const [username, setUsername] = useState<string>("");
    const [email, setEmail] = useState<string>("");
    useEffect(() => {
        axios.get(`${GETUSER}/${id}`).then(({ data }) => {
            setUsername(data.userName);
            setEmail(data.email);
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
                    {username}'s Profile
                </Typography>
                <Box component="form" noValidate sx={{ mt: 1 }}>
                    <TextField
                        margin="normal"
                        fullWidth
                        id="username"
                        label="Username"
                        name="username"
                        autoComplete="username"
                        autoFocus
                        value={username}
                        disabled
                    />
                    <TextField
                        margin="normal"
                        fullWidth
                        name="email"
                        label="Email"
                        type="email"
                        id="email"
                        autoComplete="current-email"
                        value={email}
                        disabled
                    />
                </Box>
            </Box>
        </MainLayout>
    );
}
