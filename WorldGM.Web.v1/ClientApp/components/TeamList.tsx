import * as React from 'react';
import { RouteComponentProps } from 'react-router';
import 'isomorphic-fetch';

class TeamListItemViewModel {
    id: number;
    name: string;
    city: string;
}

interface TeamListState {
    teams: TeamListItemViewModel[];
    loading: boolean;
}

export class TeamList extends React.Component<RouteComponentProps<{}>, TeamListState> {
    constructor() {
        super();

        this.state = {
            teams: [], loading: true
        }

        fetch('api/teams')
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ teams: data, loading: false });
            });
    }

    public render() {
        let teams = this.state.teams;
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : <div>
                <table className="table">
                    <thead>
                        <tr>
                            <th>City</th>
                            <th>Team</th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            teams.map(x =>
                                <tr>
                                    <td>{x.city}</td>
                                    <td><a href={'/team/' + x.id}>{x.name}</a></td>
                                </tr>
                            )
                        }
                    </tbody>
                </table>
            </div>;

        return <div>{contents}</div>;
    }
}