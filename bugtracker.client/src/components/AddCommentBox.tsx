import { useState } from "react";
import Paper from "@mui/material/Paper";
import TextField from "@mui/material/TextField";
import Grid from "@mui/material/Grid";
import Typography from "@mui/material/Typography";
import { Button } from "@mui/material";
export default function AddCommentBox({
    addCommentHandler,
}: {
    addCommentHandler: (content: string) => void;
}) {
    const [comment, setComment] = useState<string>("");
    return (
        <Paper elevation={1} sx={{ p: 2 }}>
            <Grid container spacing={3}>
                <Grid item xs={12}>
                    <Typography style={{ fontWeight: "bold" }}>
                        Add a comment
                    </Typography>
                </Grid>
                <Grid item xs={12}>
                    <TextField
                        rows={5}
                        fullWidth
                        multiline
                        value={comment}
                        onChange={(e) => {
                            setComment(e.target.value);
                        }}
                    />
                </Grid>
                <Grid item xs={12}>
                    <Button
                        variant="contained"
                        color="success"
                        sx={{ mb: 2 }}
                        onClick={() => {
                            addCommentHandler(comment);
                        }}
                    >
                        Add Comment
                    </Button>
                </Grid>
            </Grid>
        </Paper>
    );
}
