import Paper from "@mui/material/Paper";
import { Typography } from "@mui/material";
import Divider from "@mui/material/Divider";
import { IComment } from "../types/comment";
export default function CommentBox({ comment }: { comment: IComment }) {
    return (
        <Paper elevation={1} sx={{ p: 2 }}>
            <div
                style={{
                    display: "flex",
                    justifyContent: "space-between",
                }}
            >
                <Typography style={{ fontWeight: "bold" }}>
                    {comment.userName}
                </Typography>
                <Typography style={{ fontWeight: "bold" }}>
                    Last Updated:{" "}
                    {new Date(comment.updatedAt).toLocaleDateString()}
                </Typography>
            </div>
            <Divider style={{ marginTop: "10px", marginBottom: "10px" }} />
            <Typography>{comment.content}</Typography>
        </Paper>
    );
}
