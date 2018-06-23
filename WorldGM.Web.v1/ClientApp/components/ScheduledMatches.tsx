import * as React from 'react';
import 'isomorphic-fetch';
import { DefaultComponent, Schedule, ScheduledMatch } from '../WorldGm'

export class ScheduledMatches extends DefaultComponent<Schedule> {

    constructor() {
        super();

        fetch('api/Schedule')
            .then(response => {
                return response.json();
            })
            .then(data => {
                this.setState({ values: data as Schedule[], loading: false});
            })
        
    }

    public render() {

        if (this.state.values.length == 0 || this.state.loading) {
            return <div><em>Loading...</em></div>
        }
        else {
            let schedule = this.state.values[0];
            let contents = <div>
                    <h1>Schedule</h1>
                    <table className="table">
                        <thead>
                            <tr>
                                <th>Day</th>
                                <th>Match</th>
                            </tr>
                        </thead>
                        <tbody>
                        {
                            schedule.scheduledMatches.map(x =>
                                    <tr>
                                        <td>{x.seasonDay}</td>
                                        <td>
                                        <a href={'/team/' + x.awayTeamId}>{x.awayTeamName}</a> at <a href={'/team/' + x.homeTeamId}>{x.homeTeamName}</a>
                                        </td>
                                    </tr>
                                )
                            }
                        </tbody>
                    </table>
            </div>;

            return <div>{contents}</div>
        }

    }
}
