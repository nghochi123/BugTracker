import Grid from "@mui/material/Grid";
import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import { Ticket } from "../types/ticket";

interface TicketBarProps {
    ticket: Ticket;
}

export default function TicketBar({ ticket }: TicketBarProps) {
    return (
        <Grid item xs={12}>
            <Paper elevation={0} sx={{ p: 2, bgcolor: "grey.200" }}>
                <Grid container>
                    <Grid item xs={4}>
                        <Typography variant="h6" gutterBottom>
                            Priority
                        </Typography>
                        <Typography>{ticket.priority}</Typography>
                    </Grid>
                    <Grid item xs={4}>
                        <Typography variant="h6" gutterBottom>
                            Status
                        </Typography>
                        <Typography>
                            {["Closed", "Open", "In Progress"][ticket.status]}
                        </Typography>
                    </Grid>
                    <Grid item xs={4}>
                        <Typography variant="h6" gutterBottom>
                            Tags
                        </Typography>
                        {ticket.tags.map((tag) => {
                            return <Typography>{tag}</Typography>;
                        })}
                    </Grid>
                </Grid>
            </Paper>
        </Grid>
    );
}
