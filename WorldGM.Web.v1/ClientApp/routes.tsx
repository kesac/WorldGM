import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { WorldInfo } from './components/WorldInfo';
import { AthleteList } from './components/AthleteList';
import { AthleteInfo } from './components/AthleteInfo';
import { TeamList } from './components/TeamList';
import { TeamInfo } from './components/TeamInfo';
import { CityInfo } from './components/CityInfo';
import { ScheduledMatches } from './components/ScheduledMatches'

export const routes = <Layout>
    <Route exact path='/' component={Home} />
    <Route path='/worldinfo' component={WorldInfo} />
    <Route path='/athletes' component={AthleteList} />
    <Route path='/athlete/:id' component={AthleteInfo} />
    <Route path='/teams' component={TeamList} />
    <Route path='/team/:id' component={TeamInfo} />
    <Route path='/city/:id' component={CityInfo} />
    <Route path='/schedule' component={ScheduledMatches} />
</Layout>;
