import axios from "axios";
import React from "react";
import { useNavigate } from "react-router-dom";
import MainLayout from "../layout/MainLayout";
import MainTable from "../../components/Table";
import { GETPROJECTS } from "../../constants/env";
import { TableDisplay } from "../../types/tabledisplay";

export default function Home() {
    const [rows, setRows] = React.useState<TableDisplay[]>([]);
    const navigate = useNavigate();
    const handleClick = (event: React.MouseEvent<unknown>, id: number) => {
        navigate(`/projects/${id}`);
    };
    React.useEffect(() => {
        axios
            .get(GETPROJECTS, { withCredentials: true })
            .then((res) => {
                setRows(
                    // eslint-disable-next-line @typescript-eslint/no-explicit-any
                    res.data.map((item: any) => {
                        return {
                            id: item.project.id,
                            name: item.project.name,
                            description: item.project.description,
                            createdAt: new Date(item.project.createdAt),
                            updatedAt: new Date(item.project.updatedAt),
                        };
                    })
                );
            })
            .catch(() => {
                navigate("/login");
            });
    }, []);
    return (
        <MainLayout>
            <MainTable
                rows={rows}
                handleClick={handleClick}
                navigate={navigate}
                title="Projects"
                createnew="createproject"
            />
        </MainLayout>
    );
}
