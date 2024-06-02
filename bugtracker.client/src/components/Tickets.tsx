import Grid from "@mui/material/Grid";
import MainTable from "./Table";
import { TableDisplay } from "../types/tabledisplay";
import { NavigateFunction } from "react-router-dom";

interface TicketsProps {
    title: string;
    rows: TableDisplay[];
    navigate: NavigateFunction;
    handleClick: (event: React.MouseEvent<unknown>, id: number) => void;
    createNew?: string;
}

export default function Tickets(props: TicketsProps) {
    const { title, rows, handleClick, navigate, createNew = "" } = props;
    return (
        <Grid
            item
            xs={12}
            sx={{
                "& .markdown": {
                    py: 3,
                },
            }}
        >
            <MainTable
                rows={rows}
                handleClick={handleClick}
                navigate={navigate}
                title={title}
                createnew={createNew}
            />
        </Grid>
    );
}
