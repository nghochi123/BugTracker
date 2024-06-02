import Paper from "@mui/material/Paper";
import { IComment } from "../types/comment";
export default function CommentBox({ comment }: { comment: IComment }) {
    return (
        <Paper elevation={1} sx={{ p: 2 }}>
            <div>{comment.userName}</div>
            <div>{comment.content}</div>
        </Paper>
    );
}
