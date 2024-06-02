import { useNavigate } from "react-router-dom";
import Box from "@mui/material/Box";
import CssBaseline from "@mui/material/CssBaseline";
import Typography from "@mui/material/Typography";
import MainLayout from "../layout/MainLayout";
import { Button } from "@mui/material";

export default function Error() {
    const navigate = useNavigate();
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
                    An error occured.
                </Typography>
                <Button
                    variant="contained"
                    sx={{ mt: 3, mb: 2 }}
                    onClick={() => {
                        navigate("/");
                    }}
                >
                    Return to Home
                </Button>
            </Box>
        </MainLayout>
    );
}
